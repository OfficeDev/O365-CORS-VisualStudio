//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
//
//    Licensed under the MIT License (MIT);
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://mit-license.org/
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//----------------------------------------------------------------------------------------------

'use strict';
angular.module('O365CorsAngularADAL')
.controller('homeCtrl', ['$scope', 'adalAuthenticationService', '$location', function ($scope, adalService, $location) {
    $scope.login = function () {
        adalService.login();
    };
    $scope.logout = function () {
        adalService.logOut();
    };
    $scope.isActive = function (viewLocation) {        
        return viewLocation === $location.path();
    };
}]);