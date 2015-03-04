'use strict';
angular.module('O365CorsAngularADAL', ['ngRoute', 'AdalAngular'])
.config(['$routeProvider', '$httpProvider', 'adalAuthenticationServiceProvider', function ($routeProvider, $httpProvider, adalProvider) {

    $routeProvider.when("/Home", {
        controller: "homeCtrl",
        templateUrl: "/App/Views/Home.html",
    }).when("/MyFiles", {
        controller: "myFilesCtrl",
        templateUrl: "/App/Views/MyFiles.html",
        requireADLogin: true,
    }).when("/UserData", {
        controller: "userDataCtrl",
        templateUrl: "/App/Views/UserData.html",
    }).otherwise({ redirectTo: "/Home" });

    adalProvider.init(
        {
            clientId: 'acfac45e-85de-4f94-ace2-c9a95d38f505'
        },
        $httpProvider
        );

}]);
