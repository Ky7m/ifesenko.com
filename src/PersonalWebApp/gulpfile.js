/// <binding BeforeBuild='build' Clean='clean' />
'use strict';

var gulp = require('gulp');
var autoprefixer = require('gulp-autoprefixer');
var concat = require('gulp-concat');
var moreCSS = require('gulp-more-css');
var gulpif = require('gulp-if');
var imagemin = require('gulp-imagemin');
var plumber = require('gulp-plumber');
var rename = require('gulp-rename');
var replace = require('gulp-replace');
var size = require('gulp-size');
var sourcemaps = require('gulp-sourcemaps');
var uglify = require('gulp-uglify');
var gutil = require('gulp-util');
var merge = require('merge-stream');
var rimraf = require('rimraf');
var sass = require('gulp-sass');
var typescript = require('gulp-typescript');
var uncss = require('gulp-uncss');
var shorthand = require('gulp-shorthand');

var environment = {
    development: 'Development',
    staging: 'Staging',
    production: 'Production',
    current: function () {
        return process.env.ASPNETCORE_ENVIRONMENT || this.development;
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

var webroot = "./wwwroot/";
var paths = {
    bower: './bower_components/',
    scripts: 'Scripts/',
    styles: 'Styles/',
    css: webroot + 'css/',
    fonts: webroot + 'fonts/',
    img: webroot + 'images/',
    js: webroot + 'js/'
};

var sources = {
    css: [
        {
            name: 'font-awesome.css',
            copy: true,
            paths: paths.bower + 'font-awesome/css/font-awesome.min.css'
        },
        {
            name: 'bootstrap.css',
            copy: true,
            paths: paths.bower + 'bootstrap/dist/css/bootstrap.css'
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
            path: paths.bower + 'bootstrap/**/*.{ttf,svg,woff,woff2,otf,eot}'
        },
        {
            name: 'font-awesome',
            path: paths.bower + 'font-awesome/**/*.{ttf,svg,woff,woff2,otf,eot}'
        }
    ],
    img: [
        paths.img + '**/*.{png,jpg,jpeg,gif,svg}'
    ],
    js: [
        {
            name: 'bootstrap.js',
            copy: true,
            paths: paths.bower + 'bootstrap/dist/js/bootstrap.js'
        },
        {
            name: 'jquery.js',
            copy: true,
            paths: paths.bower + 'jquery/dist/jquery.js'
        },
        {
            name: 'backstretch.js',
            copy: true,
            paths: paths.bower + 'jquery-backstretch-2/jquery.backstretch.js'
        },
        {
            name: 'app.js',
            paths: paths.scripts + '**/*.ts'
        }
    ]
};

var lintSources = {
    css: paths.styles + '**/*.{css}',
    scss: paths.styles + '**/*.{scss}',
    js: paths.scripts + '**/*.js',
    ts: paths.scripts + '**/*.ts'
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


gulp.task('clean', ['clean-styles', 'clean-fonts', 'clean-code']);


gulp.task('clean-styles', function (cb) {
    return rimraf(paths.css, cb);
});


gulp.task('clean-fonts', function (cb) {
    return rimraf(paths.fonts, cb);
});

gulp.task('clean-code', function (cb) {
    return rimraf(paths.js, cb);
});

gulp.task('styles', ['clean-styles'], function () {
    var tasks = sources.css.map(function (source) {
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
              .pipe(autoprefixer({ browsers: ['last 2 version', '> 5%'] }))
              .pipe(concat(source.name))
              .pipe(sizeBefore(source.name))
              .pipe(gulpif(
                !environment.isDevelopment(),
                uncss({ html: ['views/**/*.cshtml'] })))
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
});


gulp.task('fonts', ['clean-fonts'], function () {
    var tasks = sources.fonts.map(function (source) {
        return gulp
            .src(source.path)
            .pipe(plumber())
            .pipe(rename(function (path) {
                path.dirname = '';
            }))
            .pipe(gulp.dest(paths.fonts));
    });
    return merge(tasks);
});


gulp.task('code', ['clean-code'],
function () {
    var tasks = sources.js.map(function (source) {
        if (source.copy) {
            return gulp
                .src(source.paths)
                .pipe(rename({
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.js));
        }
        else {
            var tsProject = typescript.createProject('tsconfig.json', { typescript: require('typescript') });
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
                    typescript(tsProject)))
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
});

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


gulp.task('watch', ['watch-styles', 'watch-code']);

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

gulp.task('build', ['styles', 'fonts', 'code']);

gulp.task('default', ['build']);
