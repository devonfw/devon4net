# MyThaiStar restaurant

This project was generated with [angular-cli](https://github.com/angular/angular-cli) version 1.0.0-beta.18.

**UPDATE:** Angular CLI has been updated to 1.4.9 version. Please follow the instructions at the end of this README. 

## Install or update the project

In order to update Angular CLI globally follow the nest steps:

```
$ npm uninstall -g angular-cli @angular/cli 
$ npm cache clean
$ npm install -g @angular/cli 
```

If you have a previous version of this project you must update the node modules:

Windows:
```bash
$ rmdir /s node_modules
$ rmdir /s dist
$ npm install
```

Linux or macOS:
```bash
$ rm -rf node_modules dist
$ npm install
```

To test the application as a **PWA** you will need a small http server:

```bash
$ npm i -g http-server
```

## Yarn

Project tested with the latest [Yarn](https://yarnpkg.com/lang/en/) version. Instead of using `npm` you can do the following:

```bash
$ rm -rf node_modules dist
$ yarn
```

If you have a previous version of this project run the following command:

```bash
$ yarn upgrade
```

## Run the project
There are the following alternatives in order to run My Thai Star Angular client with the different server technologies and environments:

```bash
$ npm run serve                     # OASP4J server
$ npm run serve:aot                 # AOT compilation with OASP4J server
$ npm run serve:pwa                 # Build and run the app as PWA
$ npm run serve:prod                # Production server
$ npm run serve:prod:aot            # AOT compilation with production server
$ npm run serve:prodcompose         # Production server with Docker compose
$ npm run serve:prodcompose:aot     # AOT compilation with production server with Docker compose
$ npm run serve:node                # Node.js or local Serverless server
$ npm run serve:node:aot            # AOT compilation with Node.js or local Serverless server
```

If you want to use `yarn`, use it instead of `npm run` in the above commands. 

## Build

Run `npm run build` or `yarn build` to build the project. The build artifacts will be stored in the `dist/` directory. 

- Use the `:prod` flag for a production build with AOT compilation or `:prodcompose` for a production build for the production server and Docker compose environment.
- Use the `:pwa` flag to build the application as a PWA. 

Check the different build alternatives in the `package.json` file alongside the serve scripts. 

## Running unit tests

Run `npm run test` to execute the unit tests via [Karma](https://karma-runner.github.io). Use the `:ci` flag to run tests in headless mode for CI environments. 

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).
Before running the tests make sure you are serving the app via `ng serve`.

## Further help

To get more help on the `angular-cli` use `ng --help` or go check out the [Angular-CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

