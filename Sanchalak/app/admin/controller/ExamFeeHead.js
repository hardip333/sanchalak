﻿app.controller('ExamFeeHeadCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams,  $window) {

    $rootScope.pageTitle = "Manage Exam Fee Head";

    $rootScope.showLoading = false;
     // for edit Exam Center
    $scope.editExamFeeHead = function (data) {

        $scope.examfeehead = data;
    };

    $scope.cancelExamFeeHead = function () {
        $scope.examfeehead = {
        };
    };

    $scope.cancelExamFeeHead();

    $scope.getExamFeeHeadListGet = function () {
        $rootScope.showLoading = true;

        var xml = new Object();

        $http({
            method: 'POST',
            url: 'api/ExamFeeHead/ExamFeeHeadListGet',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ExamFeeHeadListGetList = response.obj;

/*                    $scope.classMasterCount = $scope.classMasterList.length;*/

                    var data = $scope.ExamFeeHeadListGetList.slice();

                    $scope.examfeeheadTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: data
                    });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    $scope.getExamFeeHeadListGet();

    // for save
    $scope.saveExamFeeHeadAdd = function (data) {
        $rootScope.showLoading = true;
        $scope.examfeehead = data;
        $http({
            method: 'POST',
            url: 'api/ExamFeeHead/ExamFeeHeadAdd',
            data: $scope.examfeehead,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.cancelExamFeeHead();
                    $scope.getExamFeeHeadListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for edit with api 
    $scope.editExamFeeHeadEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.examfeehead = data;
        $http({
            method: 'POST',
            url: 'api/ExamFeeHead/ExamFeeHeadEdit',
            data: $scope.examfeehead,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    /*    $scope.cancelMstExam();*/
                    $scope.getExamFeeHeadListGet();
                }
            })
            .error(function (res) {
             
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save examcenter with condition
    $scope.saveExamFeeHead = function (data) {

        if (data.Id === undefined || data.Id === null || data.Id === '') {
            $scope.saveExamFeeHeadAdd(data);
        }
        else if (data.Id !== 0 || data.Id !== undefined) {
            $scope.editExamFeeHeadEdit(data);

        }

    }

    $scope.hideExamFeeHead = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamFeeHead/ExamFeeHeadIsActive',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getExamFeeHeadListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    $scope.showExamFeeHead = function (data) {
        $rootScope.showLoading = true;

        $http({
            method: 'POST',
            url: 'api/ExamFeeHead/ExamFeeHeadIsInactive',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.getExamFeeHeadListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    

});