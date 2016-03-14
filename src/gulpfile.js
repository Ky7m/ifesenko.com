/// <binding Clean='clean' BeforeBuild='build' ProjectOpened='watch'/>
'use strict';

var gulp = require('gulp'),
    fs = require('fs'),
    autoprefixer = require('gulp-autoprefixer'),
    concat = require('gulp-concat'),
    csslint = require('gulp-csslint'),
    cssnano = require('gulp-cssnano'),
    gulpif = require('gulp-if'),
    imagemin = require('gulp-imagemin'),
    jscs = require('gulp-jscs'),

    plumber = require('gulp-plumber'),
    rename = require('gulp-rename'),
    replace = require('gulp-replace'),
    size = require('gulp-size'),
    sourcemaps = require('gulp-sourcemaps'),
    uglify = require('gulp-uglify'),
    gutil = require('gulp-util'),
    merge = require('merge-stream'),

    rimraf = require('rimraf'),
    sass = require('gulp-sass'),
    scsslint = require('gulp-scss-lint'),
    typescript = require('gulp-typescript'),
    tsd = require('gulp-tsd'),
    config = require('./config.json'),
    hosting = require('./hosting.json'),
    project = require('./project.json');

// Holds information about the hosting environment.
var environment = {
    // The names of the different environments.
    development: 'Development',
    staging: 'Staging',
    production: 'Production',
    // Gets the current hosting environment the application is running under. This comes from the environment variables.
    current: function () {
        return process.env.ASPNET_ENVIRONMENT || process.env.ASPNET_ENV || this.development;
    },
    // Are we running under the development environment.
    isDevelopment: function () {
        return this.current() === this.development;
    },
    // Are we running under the staging environment.
    isStaging: function () {
        return this.current() === this.staging;
    },
    // Are we running under the production environment.
    isProduction: function () {
        return this.current() === this.production;
    }
};

// Initialize directory paths.
var paths = {
    // Source Directory Paths
    bower: './bower_components/',
    scripts: 'Scripts/',
    styles: 'Styles/',
    // Destination Directory Paths
    wwwroot: './' + hosting.webroot + '/',
    css: './' + hosting.webroot + '/css/',
    fonts: './' + hosting.webroot + '/fonts/',
    img: './' + hosting.webroot + '/img/',
    js: './' + hosting.webroot + '/js/'
};

// A TypeScript project is used to enable faster incremental compilation, rather than recompiling everything from
// scratch each time. Each resulting compiled file has it's own project which is stored in the typeScriptProjects array.
var typeScriptProjects = [];
function getTypeScriptProject(name) {
    var item;
    typeScriptProjects.forEach(function (typeScriptProject) {
        if (typeScriptProject.name === name) {
            item = typeScriptProject;
        }
    });

    if (item === undefined) {
        // Use the tsconfig.json file to specify how TypeScript (.ts) files should be compiled to JavaScript (.js).
        var typeScriptProject = typescript.createProject('tsconfig.json');
        item = {
            name: name,
            project: typeScriptProject
        };
        typeScriptProjects.push(item);
    }

    return item.project;
}

// Initialize the mappings between the source and output files.
var sources = {
    // An array containing objects required to build a single CSS file.
    css: [
        {
            // name - The name of the final CSS file to build.
            name: 'font-awesome.css',
            // copy - Just copy the file and don't run it through the minification pipeline.
            copy: true,
            // paths - The path to the file to copy.
            paths: paths.bower + 'font-awesome/css/font-awesome.min.css'
        },
        {
            // name - The name of the final CSS file to build.
            name: 'bootstrap.css',
            // copy - Just copy the file and don't run it through the minification pipeline.
            copy: true,
            // paths - The path to the file to copy.
            paths: paths.bower + 'bootstrap/dist/css/bootstrap.css'
        },
        {
            name: 'app.css',
            // paths - An array of paths to CSS or SASS files which will be compiled to CSS, concatenated and minified
            // to create a file with the above file name.
            paths: [
                paths.styles + 'site.scss'
            ]
        }
    ],
    // An array containing objects required to copy font files.
    fonts: [
        {
            // The name of the folder the fonts will be output to.
            name: 'bootstrap',
            // The source directory to get the font files from. Note that we support all font file types.
            path: paths.bower + 'bootstrap/**/*.{ttf,svg,woff,woff2,otf,eot}'
        },
        {
            name: 'font-awesome',
            path: paths.bower + 'font-awesome/**/*.{ttf,svg,woff,woff2,otf,eot}'
        }
    ],
    // An array of paths to images to be optimized.
    img: [
        paths.img + '**/*.{png,jpg,jpeg,gif,svg}'
    ],
    // An array containing objects required to build a single JavaScript file.
    js: [
        {
            name: 'bootstrap.js',
            copy: true,
            paths: paths.bower + 'bootstrap/dist/js/bootstrap.js'
        },
        {
            name: 'jquery.js',
            copy: true,
            paths: paths.bower + 'jquery/dist/jquery.min.js'
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

// Initialize the mappings between the source and output files.
var lintSources = {
    css: paths.styles + '**/*.{css}',
    scss: paths.styles + '**/*.{scss}',
    js: paths.scripts + '**/*.js',
    ts: paths.scripts + '**/*.ts'
};

// Calls and returns the result from the gulp-size plugin to print the size of the stream. Makes it more readable.
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

/*
 * Deletes all files and folders within the css directory.
 */
gulp.task('clean-css', function (cb) {
    return rimraf(paths.css, cb);
});

/*
 * Deletes all files and folders within the fonts directory.
 */
gulp.task('clean-fonts', function (cb) {
    return rimraf(paths.fonts, cb);
});

/*
 * Deletes all files and folders within the js directory.
 */
gulp.task('clean-js', function (cb) {
    return rimraf(paths.js, cb);
});

/*
 * Deletes all files and folders within the css, fonts and js directories.
 */
gulp.task('clean', ['clean-css', 'clean-fonts', 'clean-js']);

/*
 * Report warnings and errors in your CSS and SCSS files (lint them) under the Styles folder.
 */
gulp.task('lint-css', function () {
    return merge([                              // Combine multiple streams to one and return it so the task can be chained.
        gulp.src(lintSources.css)               // Start with the source .css files.
            .pipe(plumber())                    // Handle any errors.
            .pipe(csslint())                    // Get any CSS linting errors.
            .pipe(csslint.reporter()),          // Report any CSS linting errors to the console.
        gulp.src(lintSources.scss)              // Start with the source .scss files.
            .pipe(plumber())                    // Handle any errors.
            .pipe(scsslint())                   // Get and report any SCSS linting errors to the console.
    ]);
});

/*
 * Report warnings and errors in your styles and scripts (lint them).
 */
gulp.task('lint', [
    'lint-css'
]);

/*
 * Builds the CSS for the site.
 */
gulp.task('build-css', ['clean-css', 'lint-css'], function () {
    var tasks = sources.css.map(function (source) { // For each set of source files in the sources.
        if (source.copy) {                          // If we are only copying files.
            return gulp
                .src(source.paths)                  // Start with the source paths.
                .pipe(rename({                      // Rename the file to the source name.
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.css));        // Saves the CSS file to the specified destination path.
        }
        else {
            return gulp                             // Return the stream.
                .src(source.paths)                  // Start with the source paths.
                .pipe(plumber())                    // Handle any errors.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.init()))             // Set up the generation of .map source files for the CSS.
                .pipe(gulpif('**/*.scss', sass()))  // If the file is a SASS (.scss) file, compile it to CSS (.css).
                .pipe(autoprefixer({                // Auto-prefix CSS with vendor specific prefixes.
                    browsers: [
                        '> 1%',                     // Support browsers with more than 1% market share.
                        'last 2 versions'           // Support the last two versions of browsers.
                    ]
                }))
                .pipe(concat(source.name))          // Concatenate CSS files into a single CSS file with the specified name.
                .pipe(sizeBefore(source.name))      // Write the size of the file to the console before minification.
                .pipe(gulpif(
                    !environment.isDevelopment(),   // If running in the staging or production environment.
                    cssnano()))                     // Minifies the CSS.
                .pipe(sizeAfter(source.name))       // Write the size of the file to the console after minification.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.write('.')))         // Generates source .map files for the CSS.
                .pipe(gulp.dest(paths.css));        // Saves the CSS file to the specified destination path.
        }
    });
    return merge(tasks);                            // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Builds the font files for the site.
 */
gulp.task('build-fonts', ['clean-fonts'], function () {
    var tasks = sources.fonts.map(function (source) { // For each set of source files in the sources.
        return gulp                             // Return the stream.
            .src(source.path)                   // Start with the source paths.
            .pipe(plumber())                    // Handle any errors.
            .pipe(rename(function (path) {      // Rename the path to remove an unnecessary directory.
                path.dirname = '';
            }))
            .pipe(gulp.dest(paths.fonts));      // Saves the font files to the specified destination path.
    });
    return merge(tasks);                        // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Builds the JavaScript files for the site.
 */
gulp.task('build-js', [
    'clean-js'
],
function () {
    var tasks = sources.js.map(function (source) {  // For each set of source files in the sources.
        if (source.copy) {                          // If we are only copying files.
            return gulp
                .src(source.paths)                  // Start with the source paths.
                .pipe(rename({                      // Rename the file to the source name.
                    basename: source.name,
                    extname: ''
                }))
                .pipe(gulp.dest(paths.js));         // Saves the JavaScript file to the specified destination path.
        }
        else {
            return gulp                             // Return the stream.
                .src(source.paths)                  // Start with the source paths.
                .pipe(plumber())                    // Handle any errors.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.init()))             // Set up the generation of .map source files for the JavaScript.
                .pipe(gulpif(
                    source.replacement,             // If the source has a replacement to be made.
                    replace(                        // Carry out the specified find and replace.
                        source.replacement ? source.replacement.find : '',
                        source.replacement ? source.replacement.replace : '')))
                .pipe(gulpif(                       // If the file is a TypeScript (.ts) file.
                    '**/*.ts',
                    typescript(getTypeScriptProject(source)))) // Compile TypeScript (.ts) to JavaScript (.js) using the specified options.
                .pipe(concat(source.name))          // Concatenate JavaScript files into a single file with the specified name.
                .pipe(sizeBefore(source.name))      // Write the size of the file to the console before minification.
                .pipe(gulpif(
                    !environment.isDevelopment(),   // If running in the staging or production environment.
                    uglify()))                      // Minifies the JavaScript.
                .pipe(sizeAfter(source.name))       // Write the size of the file to the console after minification.
                .pipe(gulpif(
                    environment.isDevelopment(),    // If running in the development environment.
                    sourcemaps.write('.')))         // Generates source .map files for the JavaScript.
                .pipe(gulp.dest(paths.js));         // Saves the JavaScript file to the specified destination path.
        }
    });
    return merge(tasks);                            // Combine multiple streams to one and return it so the task can be chained.
});

/*
 * Cleans and builds the CSS, Font and JavaScript files for the site.
 */
gulp.task('build', [
    'build-css',
    'build-fonts',
    'build-js'
]);

/*
 * Optimizes and compresses the GIF, JPG, PNG and SVG images for the site.
 */
gulp.task('optimize-images', function () {
    return gulp
        .src(sources.img)                   // Start with the source paths.
        .pipe(plumber())                    // Handle any errors.
        .pipe(sizeBefore())                 // Write the size of the file to the console before minification.
        .pipe(imagemin({                    // Optimize the images.
            multipass: true,                // Optimize svg multiple times until it's fully optimized.
            optimizationLevel: 7            // The level of optimization (0 to 7) to make, the higher the slower it is.
        }))
        .pipe(gulp.dest(paths.img))         // Saves the image files to the specified destination path.
        .pipe(sizeAfter());                 // Write the size of the file to the console after minification.
});

/*
 * Watch the styles folder for changes to .css, or .scss files. Build the CSS if something changes.
 */
gulp.task('watch-css', function () {
    return gulp
        .watch(
            paths.styles + '**/*.{css,scss}',   // Watch the styles folder for file changes.
            ['build-css'])                      // Run the build-css task if a file changes.
        .on('change', function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue('File ' + event.path + ' was ' + event.type + ', build-css task started.'));
        });
});

/*
 * Watch the scripts folder for changes to .js or .ts files. Build the JavaScript if something changes.
 */
gulp.task('watch-js', function () {
    return gulp
        .watch(
            paths.scripts + '**/*.{js,ts}',     // Watch the scripts folder for file changes.
            ['build-js'])                       // Run the build-js task if a file changes.
        .on('change', function (event) {        // Log the change to the console.
            gutil.log(gutil.colors.blue('File ' + event.path + ' was ' + event.type + ', build-js task started.'));
        });
});

/*
 * Watch the styles and scripts folders for changes. Build the CSS and JavaScript if something changes.
 */
gulp.task('watch', ['watch-css', 'watch-js']);
gulp.task('tsd', function () {
    return gulp.src('./gulp_tsd.json').pipe(tsd());
});
/*
 * The default gulp task. This is useful for scenarios where you are not using Visual Studio. Does a full clean and
 * build before watching for any file changes.
 */
gulp.task(
    'default',
    [
        'tsd',
        'build'
    ]);