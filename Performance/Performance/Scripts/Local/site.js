function DateToYMD(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var twoDigitMonth = ("00" + month).slice(-2);
    var day = date.getDate();
    var twoDigitDay = ("00" + day).slice(-2);
    return year + "-" + twoDigitMonth + "-" + twoDigitDay;
}

var mainApp = angular.module("performanceApp", [])
    .controller("statController", function ($scope, $http) {
        $scope.update = function () {
            var url = baseUrl + $scope.employeeId;
            $scope.startDateDescription = "Today";
            $scope.endDateDescription = "Today";
            if ($scope.startDate) {
                $scope.startDateDescription = DateToYMD($scope.startDate);
                url += "/" + $scope.startDateDescription;
                if ($scope.endDate) {
                    $scope.endDateDescription = DateToYMD($scope.endDate);
                    url += "/" + $scope.endDateDescription;
                }
            }
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