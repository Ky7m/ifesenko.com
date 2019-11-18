'use strict';
const gulp = require('gulp');
const autoprefixer = require('gulp-autoprefixer');
const concat = require('gulp-concat');
const moreCSS = require('gulp-more-css');
const gulpif = require('gulp-if');
const imagemin = require('gulp-imagemin');
const plumber = require('gulp-plumber');
const rename = require('gulp-rename');
const replace = require('gulp-replace');
const size = require('gulp-size');
const sourcemaps = require('gulp-sourcemaps');
const uglify = require('gulp-uglify');
const gutil = require('gulp-util');
const merge = require('merge-stream');
const rimraf = require('gulp-rimraf');
const sass = require('gulp-sass');
const typescript = require('gulp-typescript');
const shorthand = require('gulp-shorthand');

const environment = {
    development: 'Development',
    staging: 'Staging',
    production: 'Production',
    current: function () {
        return process.env.ASPNETCORE_ENVIRONMENT || this.production;
    },
    isDevelopment: function () {
        return this.current() === this.development;
    },
    isStaging: function () {
        return this.current() === this.staging;
    },
    isProduction: function () {
        return this.current() === this.production;
    }
};

const webroot = "./wwwroot/";
const paths = {
    npm: './node_modules/',
    scripts: 'Scripts/',
    styles: 'Styles/',
    css: webroot + 'css/',
    fonts: webroot + 'fonts/',
    img: webroot + 'images/',
    js: webroot + 'js/'
};

const sources = {
    css: [
        {
            name: 'bootstrap.css',
            copy: true,
            paths: paths.npm + 'bootstrap/dist/css/bootstrap.css'
        },
        {
            name: 'app.css',
            paths: [
                paths.styles + 'site.scss'
            ]
        }
    ],
    fonts: [
        {
            name: 'bootstrap',
            path: paths.npm + 'bootstrap/**/*.{ttf,svg,woff,woff2,otf,eot}'
        },
        {
            name: 'fonts',
            path: 'Fonts/**/*.{ttf,svg,woff,woff2,otf,eot}'
        }
    ],
    img: [
        paths.img + '**/*.{png,jpg,jpeg,gif,svg}'
    ],
    js: [
        {
            name: 'bootstrap.js',
            copy: true,
            paths: paths.npm + 'bootstrap/dist/js/bootstrap.js'
        },
        {
            name: 'jquery.js',
            copy: true,
            paths: paths.npm + 'jquery/dist/jquery.js'
        },
        {
            name: 'backstretch.js',
            copy: true,
            paths: paths.npm + 'jquery-backstretch/jquery.backstretch.js'
        },
        {
            name: 'app.js',
            paths: paths.scripts + '**/*.ts'
        }
    ]
};

function sizeBefore(title) {
    return size({
        title: 'Before: ' + title
    });
}

function sizeAfter(title) {
    return size({
        title: 'After: ' + title
    });
}

gulp.task('clean-styles', function () {
    return gulp.src(paths.css, {read: false})
        .pipe(rimraf());
});

gulp.task('clean-fonts', function () {
    return gulp.src(paths.fonts, {read: false})
        .pipe(rimraf());
});

gulp.task('clean-code', function () {
    return gulp.src(paths.js, {read: false})
        .pipe(rimraf());
});

gulp.task('clean', gulp.series('clean-styles', 'clean-fonts', 'clean-code'));

gulp.task('styles', gulp.series('clean-styles', function () {
    const tasks = sources.css.map(function (source) {
        if (source.copy) {
            return gulp
                .src(source.paths)
                .pipe(rename({
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.css));
        } else {
            return gulp
                .src(source.paths)
                .pipe(plumber())
                .pipe(gulpif(
                    environment.isDevelopment(),
                    sourcemaps.init()))
                .pipe(gulpif('**/*.scss', sass()))
                .pipe(autoprefixer({browsers: ['last 2 version', '> 5%']}))
                .pipe(concat(source.name))
                .pipe(sizeBefore(source.name))
                .pipe(gulpif(
                    !environment.isDevelopment(),
                    shorthand()))
                .pipe(gulpif(
                    !environment.isDevelopment(),
                    moreCSS()))
                .pipe(sizeAfter(source.name))
                .pipe(gulpif(
                    environment.isDevelopment(),
                    sourcemaps.write('.')))
                .pipe(gulp.dest(paths.css));
        }
    });
    return merge(tasks);
}));

gulp.task('fonts', gulp.series('clean-fonts', function () {
    const tasks = sources.fonts.map(function (source) {
        return gulp
            .src(source.path)
            .pipe(plumber())
            .pipe(rename(function (path) {
                path.dirname = '';
            }))
            .pipe(gulp.dest(paths.fonts));
    });
    return merge(tasks);
}));

gulp.task('code', gulp.series('clean-code', function () {
    const tasks = sources.js.map(function (source) {
        if (source.copy) {
            return gulp
                .src(source.paths)
                .pipe(rename({
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.js));
        } else {
            const tsProject = typescript.createProject('tsconfig.json', {typescript: require('typescript')});
            return gulp
                .src(source.paths)
                .pipe(plumber())
                .pipe(gulpif(
                    environment.isDevelopment(),
                    sourcemaps.init()))
                .pipe(gulpif(
                    source.replacement,
                    replace(
                        source.replacement ? source.replacement.find : '',
                        source.replacement ? source.replacement.replace : '')))
                .pipe(gulpif(
                    '**/*.ts',
                    tsProject()))
                .pipe(concat(source.name))
                .pipe(sizeBefore(source.name))
                .pipe(gulpif(
                    !environment.isDevelopment(),
                    uglify()))
                .pipe(sizeAfter(source.name))
                .pipe(gulpif(
                    environment.isDevelopment(),
                    sourcemaps.write('.')))
                .pipe(gulp.dest(paths.js));
        }
    });
    return merge(tasks);
}));

gulp.task('images', function () {
    return gulp
        .src(sources.img)
        .pipe(plumber())
        .pipe(sizeBefore())
        .pipe(imagemin({
            multipass: true,
            optimizationLevel: 4
        }))
        .pipe(gulp.dest(paths.img))
        .pipe(sizeAfter());
});

gulp.task('watch-styles', function () {
    return gulp
        .watch(
            paths.styles + '**/*.{css,scss}',
            ['styles'])
        .on('change', function (event) {
            gutil.log(gutil.colors.blue('File ' + event.path + ' was ' + event.type + ', styles task started.'));
        });
});


gulp.task('watch-code', function () {
    return gulp
        .watch(
            paths.scripts + '**/*.{js,ts}',
            ['code'])
        .on('change', function (event) {
            gutil.log(gutil.colors.blue('File ' + event.path + ' was ' + event.type + ', code task started.'));
        });
});

gulp.task('watch', gulp.series('watch-styles', 'watch-code'));

gulp.task('build', gulp.series('styles', 'fonts', 'code'));

gulp.task('default', gulp.series('build'));