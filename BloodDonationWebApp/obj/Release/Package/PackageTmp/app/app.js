
var app = angular.module('BloodDonationApp', ['ngRoute']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/contact", {
        controller: "contactController",
        templateUrl: "/app/views/contact.html"
    });
    $routeProvider.when("/register", {
        controller: "registerController",
        templateUrl: "/app/views/register.html"
    });

    $routeProvider.when("/bloodfacts", {
        controller: "bloodFactsController",
        templateUrl: "/app/views/bloodfacts.html"
    });

    $routeProvider.when("/bloodtips", {
        controller: "bloodTipsController",
        templateUrl: "/app/views/bloodtips.html"
    });
    $routeProvider.otherwise({ redirectTo: "/home" });

});

app.run(function ($rootScope) {
    $rootScope.layoutClass = "";
    $rootScope.title = "";
    $rootScope.fixHeight = "";
    $rootScope.ChangeHeight = function () {
        var fh = $('.fullscreen-wrapper, .header-page-wrapp').outerHeight();
        $rootScope.fixHeight = fh;
    };

});
var serviceBase = 'http://localhost:9175/';

