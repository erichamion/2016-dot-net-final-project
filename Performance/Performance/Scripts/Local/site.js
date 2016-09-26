var mainApp = angular.module("performanceApp", [])
    .controller("statController", function ($scope, $http) {
        $scope.update = function () {
            var url = baseUrl +  $scope.employeeId;
            $scope.header = $scope.employeeId ? $scope.employeeId + ": Today" : "Please choose an employee";
            $http.get(url)
                .success(function (resp) {
                    $scope.stats = resp;
                })
                .error(function(resp) {
                    $scope.stats = undefined;
                });
        };

        var baseUrl = "/api/Stats/";
        $scope.update();

    });