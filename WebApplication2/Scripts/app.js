﻿
var webApp = angular.module("webApp", ["ngRoute", "ngMessages"]);
webApp.config(["$routeProvider", "$locationProvider",
    function ($routeProvider, $locationProvider) {
        $routeProvider.
            when('/', {
                templateUrl: 'template/mainpage',
                controller: 'RedirectCtrl'//,
                //resolve: {
                //    auth: ["Security", function (security) {
                //        return security.checkUserIsAuthorized();
                //    }],
                //    access: ["Security", function (security) {
                //        return security.checkUserInRole(['developer', 'admin']);
                //    }]
                //}
            }).
             when('/login', {
                 templateUrl: 'template/account/login',
                 controller: 'LoginCtrl'
             }).
             when('/hall', {
                 templateUrl: 'template/app/hall',
                 controller: 'HallCtrl'
             }).
            when('/register', {
                templateUrl: 'template/account/register',
                controller: 'LoginCtrl'
            })
            .when('/dog', {
                templateUrl: 'template/app/dog',
                controller: 'DogCtrl'
            }).
            when('/adddog', {
                templateUrl: 'template/app/adddog',
                controller: 'DogCtrl'
            }).
            otherwise({
                redirectTo: '/'
            });

        $locationProvider.html5Mode(true);
    }]);