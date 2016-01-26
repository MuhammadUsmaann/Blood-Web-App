'use strict';
app.controller('homeController', ['$scope', '$rootScope', function ($scope, $rootScope) {
    $rootScope.layoutClass = "";
    $rootScope.title = "Home";
    $rootScope.ChangeHeight();
    
}]);