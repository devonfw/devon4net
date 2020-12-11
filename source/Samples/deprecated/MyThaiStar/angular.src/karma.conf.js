// Karma configuration file, see link for more information
// https://karma-runner.github.io/0.13/config/configuration-file.html

module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine', '@angular/cli'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage-istanbul-reporter'),
      require('karma-firefox-launcher'),
      require('@angular/cli/plugins/karma')
    ],
    browserNoActivityTimeout: 60000,
    customLaunchers: {
      // chrome setup for travis CI using chromium
      Chrome_travis_ci: {
        base: 'Chrome',
        flags: ['--no-sandbox']
      },
      ChromeHeadless:  {
        base:   'Chrome',
        flags:  [
          '--headless',
          '--disable-gpu',
          // Without a remote debugging port, Google Chrome exits immediately.
          '--remote-debugging-port=9222',
          '--no-sandbox'
        ],
      }
    },
    client: {
      clearContext: false // leave Jasmine Spec Runner output visible in browser
    },
    files: [
      // Include all Angular dependencies
      {pattern: 'node_modules/@angular/material/prebuilt-themes/indigo-pink.css'},
      {pattern: 'node_modules/@angular/**/*', included: false, watched: false},
      {pattern: 'node_modules/rxjs/**/*.js', included: false, watched: false},
      {pattern: './src/test.ts', watched: false },
    ],
    preprocessors: {
      './src/test.ts': ['@angular/cli']
    },
    mime: {
      'text/x-typescript': ['ts', 'tsx']
    },
    coverageIstanbulReporter: {
      reports: ['html', 'lcovonly'],
      fixWebpackSourcePaths: true
    },
    angularCli: {
      environment: 'dev'
    },
    reporters: config.angularCli && config.angularCli.codeCoverage ?
      ['progress', 'coverage-istanbul'] :
      ['progress', 'kjhtml'],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    browsers: ['Chrome', 'ChromeHeadless', 'PhantomJS'],
/*    autoWatch: true,
    singleRun: false
*/  });
};
