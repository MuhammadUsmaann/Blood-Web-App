'use strict';


//<input type="password" id="pw1" name="pw1" 
//ng-model="pw1" ng-required/>
//<input type="password" id="pw2" name="pw2" 
//ng-model="pw2" ng-required pw-check="pw1"/>

//angular.module('myApp.directives', [])
//  .directive('pwCheck', [function () {
//      return {
//          require: 'ngModel',
//          link: function (scope, elem, attrs, ctrl) {
//              var firstPassword = '#' + attrs.pwCheck;
//              elem.add(firstPassword).on('keyup', function () {
//                  scope.$apply(function () {
//                      var v = elem.val() === $(firstPassword).val();
//                      ctrl.$setValidity('pwmatch', v);
//                  });
//              });
//          }
//      }
//  }]);

app.controller('registerController', ['$scope', '$rootScope', "SignUpService", function ($scope, $rootScope, SignUpService) {
    $rootScope.layoutClass = "contact-form fixed-nav";
    $rootScope.fixHeight = 0;

    $scope.data = {
        UserName: "",
        Email: "",
        Password: "",
        ConfirmPassword: "",
        ContactNo: "",
        Role: "",
    }
    $scope.SignUpClick = function () {

        var result = SignUpService($scope.data);
        result.then(function (result) {
            if (result.success) {
                if ($scope === undefined) {
                    $location.path('/home');
                } else {
                    
                }
            } else {
                
            }
        });
    }
}]);


app.factory('SignUpService', ['$http','$q', function ($http,$q) {
    return function (params) {
        var deferredObject = $q.defer();
        $http.post('api/Account/Register', params)
             .success(function (data) {
            if (data == "True") {
                deferredObject.resolve({ success: true });
            } else {
                deferredObject.resolve({ success: false });
            }
        }).
        error(function () {
            deferredObject.resolve({ success: false });
        });
        return deferredObject.promise;
    }

}]);