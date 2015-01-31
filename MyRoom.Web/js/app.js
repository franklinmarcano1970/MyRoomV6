'use strict';
var serviceBase      = 'http://localhost:49698/';
//var serviceBaseWeb‏  ‏ = 'http://localhost:35269/';
//var serviceBase = 'http://management-webapi-myroom.azurewebsites.net/';
//var serviceBase = 'http://servicioswebmoviles.azurewebsites.net/';
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
    rootFile : 'images/catalogs/',
    rootFileProduct : 'images/products/'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);