app.controller('ExamConfigStatusCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Exam Config Status";

    $rootScope.showLoading = false;

    //$scope.editMstExamCenterAdd = function (data) {

    //    $scope.examcenter = data;
    //};

    $scope.cancelExamconfig = function () {
        $scope.examcenter = {

        };
  
    };

    $scope.cancelExamconfig();

    //$scope.getMstExamCenterGet = function () {
    //    $rootScope.showLoading = true;

    //    var xml = new Object();

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamCenter/MstExamCenterGet',
    //        data: xml,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code != "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                $scope.MstExamCenter = response.obj;

    //                /*  $scope.classMasterCount = $scope.classMasterList.length;*/

    //                var data = $scope.MstExamCenter.slice();

    //                $scope.examcenterTableParams = new NgTableParams({
    //                    count: 1000
    //                }, {
    //                    dataset: data
    //                });
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};
    //$scope.getMstExamCenterGet();

    //$scope.saveMstExamCenterAdd = function () {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamCenter/MstExamCenterAdd ',
    //        data: $scope.examcenter,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.cancelMstExamCenterAdd();
    //                $scope.getMstExamCenterGet();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    //$scope.enableMstExamIsActiveEnable = function (data) {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamCenter/MstExamCenterIsActiveEnable  ',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getMstExamCenterGet();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    //$scope.disableMstExamIsActiveDisable = function (data) {
    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamCenter/MstExamCenterIsActiveDisable',
    //        data: data,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getMstExamCenterGet();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};



});