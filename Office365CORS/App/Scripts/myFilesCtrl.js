'use strict';
angular.module('O365CorsAngularADAL')
.controller('myFilesCtrl', ['$scope', '$http', 'adalAuthenticationService', function ($scope, $http, adalService) {
    $scope.model = "";
    $scope.discoveryService = "";
    $scope.fileResult = "";
    $scope.loadingMessage = "Loading....";
    $scope.myFilesServiceEndpointUri = "";

    //Get Discovery Service
    $scope.getDiscoveryResource = function () {
        $http.get('/api/DiscoveryResource')
            .success(function (data, status, headers, config) {
                $scope.discoveryService = data;
                //Pass endpoints to adalService
                adalService.config.endpoints = {};
                $scope.discoveryService.forEach(function (discoveryService) {
                    adalService.config.endpoints[discoveryService.ServiceEndpointUri] = discoveryService.ServiceResourceId;
                })
                $scope.getMyFilesServiceEndpointUri();
            }).error(function (data, status, header, config) {
                alert("Error getting discovery resource: " + status);
                $scope.loadingMessage = "";
            });
    }

    //Get MyFiles ServiceEndpointUri
    $scope.getMyFilesServiceEndpointUri = function () {
        var myFilesDiscoveryService = $scope.discoveryService.filter(function (d) {
            return d.CapabilityName == "MyFiles";
        });
        $scope.myFilesServiceEndpointUri = myFilesDiscoveryService[0].ServiceEndpointUri;
        $scope.getFiles();
    };

    //Get list of my files
    $scope.getFiles = function () {
        $http.get($scope.myFilesServiceEndpointUri + '/files')
            .success(function (data, status, headers, config) {
                $scope.loadingMessage = "";
                $scope.model = data.value;
            }).error(function (data, status, header, config) {
                alert("Error getting my files: " + status);
                $scope.loadingMessage = "";
            });
    };

    //Get file content
    $scope.viewContent = function (file) {
        $http.get($scope.myFilesServiceEndpointUri + '/files/' + file.id + '/content')
            .success(function (data, status, headers, config) {
                $scope.fileResult = data;
            }).error(function (data, status, header, config) {
                alert("Error getting file content: " + status);
            });
    };

    //Get file or folder properties
    $scope.getProperties = function (file) {
        $http.get($scope.myFilesServiceEndpointUri + '/files/' + file.id)
            .success(function (data, status, headers, config) {
                $scope.fileResult = data;
            }).error(function (data, status, header, config) {
                alert("Error getting properties: " + status);
            });
    };

    //Delete file or folder
    $scope.deleteFile = function (file) {
        $http.delete($scope.myFilesServiceEndpointUri + '/files/' + file.id, {
            headers: {
                "if-match": file.eTag
            }
        }).success(function (data, status, headers, config) {
            $scope.getFiles();
        }).error(function (data, status, header, config) {
            alert("Error deleting: " + status);
        });
    };

}]);