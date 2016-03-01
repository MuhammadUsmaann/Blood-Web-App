
var app = angular.module('BloodDonationApp', ['ngRoute']);

var serviceBase = 'http://localhost:9175/';


app.config(function ($routeProvider) {

    var baseUrl = $('base').attr('href');

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: baseUrl + "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: baseUrl + "/app/views/login.html"
    });

    $routeProvider.when("/contact", {
        controller: "contactController",
        templateUrl: baseUrl + "/app/views/contact.html"
    });

    $routeProvider.when("/register", {
        controller: "registerController",
        templateUrl: baseUrl + "/app/views/register.html"
    });

    $routeProvider.when("/bloodfacts", {
        controller: "bloodFactsController",
        templateUrl: baseUrl + "/app/views/bloodfacts.html"
    });

    $routeProvider.when("/bloodtips", {
        controller: "bloodTipsController",
        templateUrl: baseUrl + "/app/views/bloodtips.html"
    });

    $routeProvider.when("/advices", {
        controller: "doctoradvicesController",
        templateUrl: baseUrl + "/app/views/doctoradvices.html"
    });

    $routeProvider.when("/results", {
        controller: "searchresultsController",
        templateUrl: baseUrl + "/app/views/searchresults.html"
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

