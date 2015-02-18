'use strict';

/* Controllers */
// signin controller
app.controller('SigninFormController', ['$scope', '$http', '$state', 'authService', 'ngAuthSettings', function ($scope, $http, $state, authService, ngAuthSettings) {
    $scope.user = {
        username: "",
        password: "",
        useRefreshTokens: true
    };
    if (authService.authentication.isAuth && $state.$current.name == "access.signin") {
        $state.go('app.dashboard-v1');
    }
   
    $scope.user.username = authService.authentication.username;
    $scope.login = function () {
        $scope.authError = '';
        authService.login($scope.user).then(function (response) {
            $state.go('app.dashboard-v1');
        },
        function (err) {
            if (!err)
                $scope.authError = 'DATABASE CONNECTION REFUSSED';
            else
                $scope.authError = err.error_description;
        });

    }

    $scope.logOut = function () {
        authService.logOut();
        $state.go('access.signin');
    }

    //$scope.authentication = authService.authentication;
}]);