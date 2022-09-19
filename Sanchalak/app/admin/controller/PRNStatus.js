app.controller('PRNStatusCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage PRN Status";

    $rootScope.showLoading = false;


    $scope.cancelPRN = function () {
        $scope.filter = {
            ExamMasterId: 0,
        };
    };

    $scope.cancelPRN();

    //// for get inward table 

    //$scope.getpendingGetPendingExamInwardList = function () {

    //    $rootScope.showLoading = true;
        
    //    //var xml = new Object();
    //    //xml.ProgrammeId = $scope.filter.ProgrammeId;
    //    //xml.FacultyExamMapId = $scope.selectedSchedule.Id;
    //    //xml.ExamMasterId = $scope.selectedSchedule.ExamMasterId;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamFormMaster/GetPendingExamInwardList',
    //        data: $scope.filter,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code != "200") {
    //                alert(response.obj);
    //            }
    //            else {
    //                $scope.GetPendingExamInwardListlist = response.obj;

    //                $scope.inwardTableParams = new NgTableParams({
    //                    count: 1000
    //                }, {
    //                    dataset: $scope.GetPendingExamInwardListlist
    //                });
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

    //// for update button in table 
    //$scope.saveupdateExamInward = function () {

    //    $rootScope.showLoading = true;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamFormMaster/updateExamInward',
    //        data: $scope.GetPendingExamInwardListlist,
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {

    //            $rootScope.showLoading = false;

    //            if (response.response_code !== "200") {

    //                alert(response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.getpendingGetPendingExamInwardList();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};
});