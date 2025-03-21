'use strict';
import gulp from 'gulp';
import debug from 'gulp-debug';
const { task, src, series, dest, watch } = gulp;
import autoprefixer from 'gulp-autoprefixer';
import concat from 'gulp-concat';
import gulpif from 'gulp-if';
import plumber from 'gulp-plumber';
import rename from 'gulp-rename';
import replace from 'gulp-replace';
import size from 'gulp-size';
import uglify from 'gulp-uglify';
import util from 'gulp-util';
const { log, colors } = util;
import merge from 'merge-stream';
import {deleteAsync} from 'del';
import * as dartSass from 'sass';
import gulpSass from 'gulp-sass';
const sass = gulpSass(dartSass);
import ts from 'gulp-typescript';
const { createProject } = ts;

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

task('clean-styles', function () {
    return deleteAsync(paths.css);
});

task('clean-code', function () {
    return deleteAsync(paths.js);
});

task('clean', series('clean-styles', 'clean-code'));

task('styles', series('clean-styles', function () {
    const tasks = sources.css.map(function (source) {
        if (source.copy) {
            return src(source.paths)
                .pipe(rename({
                    basename: source.name,
                    extname: ''
                }))
                .pipe(dest(paths.css));
        } else {
            return src(source.paths)
                .pipe(plumber())
                .pipe(gulpif('**/*.scss', sass().on('error', sass.logError)))
                .pipe(autoprefixer())
                .pipe(concat(source.name))
                .pipe(sizeBefore(source.name))
                .pipe(sizeAfter(source.name))
                .pipe(dest(paths.css))
                .pipe(debug());
        }
    });
    return merge(tasks);
}));

task('code', series('clean-code', function () {
    const tasks = sources.js.map(function (source) {
        if (source.copy) {
            return src(source.paths)
                .pipe(rename({
                    basename: source.name,
                    extname: ''
                }))
                .pipe(dest(paths.js));
        } else {
            const tsProject = createProject('tsconfig.json');
            return src(source.paths)
                .pipe(plumber())
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
                .pipe(dest(paths.js))
                .pipe(debug());
        }
    });
    return merge(tasks);
}));

task('watch-styles', function () {
    return watch(
            paths.styles + '**/*.{css,scss}',
            ['styles'])
        .on('change', function (event) {
            log(colors.blue('File ' + event.path + ' was ' + event.type + ', styles task started.'));
        });
});


task('watch-code', function () {
    return watch(
            paths.scripts + '**/*.{js,ts}',
            ['code'])
        .on('change', function (event) {
            log(colors.blue('File ' + event.path + ' was ' + event.type + ', code task started.'));
        });
});

task('watch', series('watch-styles', 'watch-code'));

task('build', series('styles', 'code'));

task('default', series('build'));