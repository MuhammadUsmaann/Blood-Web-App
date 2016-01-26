'use strict';
app.controller('loginController', ['$scope', '$rootScope', function ($scope, $rootScope) {
    $rootScope.layoutClass = "contact-form fixed-nav";
    $rootScope.fixHeight = 0;
    $rootScope.title = "Login";
}]);