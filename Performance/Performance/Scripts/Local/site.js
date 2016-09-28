﻿function DateToYMD(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var twoDigitMonth = ("00" + month).slice(-2);
    var day = date.getDate();
    var twoDigitDay = ("00" + day).slice(-2);
    return year + "-" + twoDigitMonth + "-" + twoDigitDay;
}



var mainApp = angular.module("performanceApp", [], function ($httpProvider) {
    // Code to make POST requests use url-encoded string instead of JSON
    // from http://victorblog.com/2012/12/20/make-angularjs-http-service-behave-like-jquery-ajax/


    // Use x-www-form-urlencoded Content-Type
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

    /**
    * The workhorse; converts an object to x-www-form-urlencoded serialization.
    * @param {Object} obj
    * @return {String}
    */ 
    var param = function(obj) {
    var query = '', name, value, fullSubName, subName, subValue, innerObj, i;
      
    for(name in obj) {
        value = obj[name];
        
        if(value instanceof Array) {
        for(i=0; i<value.length; ++i) {
            subValue = value[i];
            fullSubName = name + '[' + i + ']';
            innerObj = {};
            innerObj[fullSubName] = subValue;
            query += param(innerObj) + '&';
        }
        }
        else if(value instanceof Object) {
        for(subName in value) {
            subValue = value[subName];
            fullSubName = name + '[' + subName + ']';
            innerObj = {};
            innerObj[fullSubName] = subValue;
            query += param(innerObj) + '&';
        }
        }
        else if(value !== undefined && value !== null)
        query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
    }
      
    return query.length ? query.substr(0, query.length - 1) : query;
    };

    // Override $http service's default transformRequest
    $httpProvider.defaults.transformRequest = [function(data) {
    return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
    }];
});



mainApp.controller("statController", function ($scope, $http) {
    $scope.update = function () {
        var url = $scope.baseUrl + $scope.employeeId;
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

    $scope.baseUrl = "/api/Stats/";
    $scope.update();

    })
    .controller("loginController", function ($scope, $http) {
        $scope.submitLogin = function (loginInfo) {
            $http.post(
                $scope.loginUrl, 
                { username: loginInfo.employeeId, password: loginInfo.password, grant_type: 'password' }
                )
                .success(function (resp) {
                    $scope.$parent.token = resp.access_token;
                    $scope.$parent.loggedInEmployeeId = loginInfo.employeeId;
                })
                .error(function(resp) {
                    alert("Login failed");
                });
        };

        

        $scope.loginUrl = "/Token";

    });