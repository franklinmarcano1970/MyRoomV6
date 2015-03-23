'use strict';
var serviceBase      = 'http://localhost:49698/';
//var serviceBase = 'http://management-webapi-myroom.azurewebsites.net/';
//var serviceBase = 'http://management-webapi-myroom.azurewebsites.net/';
var app = angular.module('app', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngTouch',
    'ngStorage',
    'ui.router',
    'ui.bootstrap',
    'ui.utils',
    'ui.load',
    'ui.jq',
    'ui.validate',
    'oc.lazyLoad',
    'pascalprecht.translate',
    'LocalStorageModule'
]);

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});
app.constant('ngWebBaseSettings', {
    webServiceBase: 'http://localhost:35269/',
    rootFileHotel : 'images/hotels/',
    rootFile : 'images/',
    rootFileProduct: 'images/',
    fileSize: 512000
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

/*!
 * angular-datatables - v0.3.0
 * https://github.com/l-lin/angular-datatables
 * License: MIT
 */
