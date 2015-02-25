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
            tenant: 'rohiagra.ccsctp.net',
            clientId: 'ddac6a49-6f7c-40df-954e-971d6aedfe83',
            instance: 'https://login.windows-ppe.net/'
        },
        $httpProvider
        );

}]);
