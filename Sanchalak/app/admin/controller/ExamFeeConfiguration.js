app.controller('ExamFeeConfigurationCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Exam Fee Configuration";

    $rootScope.showLoading = false;

    $scope.cancelMstExamFeesConfigurationHeadMapAdd = function () {
        $scope.filter = {
            AcademicYearId: 0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0,
            ProgrammePartTermId:0
        };
    };

    $scope.cancelMstExamFeesConfigurationHeadMapAdd();

    // for radio button
    $scope.getApperanceTypeGetByProgPartTermId = function (data) {
        $rootScope.showLoading = true;

        var xml = new Object();
        xml.ProgrammePartTermId = $scope.filter.ProgrammePartTermId;
        xml.FacultyExamMapId = $scope.filter.FacultyExamMapId;

        $http({
            method: 'POST',
        /*    url: 'api/AppearanceTypeMaster/MstAppearanceTypeGetActive',*/
            url: 'api/AppearanceTypeMaster/ApperanceTypeGetByProgPartTermId',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
          
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
               
                    alert(response.obj);
                }
                else {
                 
                    $scope.ApperanceTypeGetByProgPartTermIdList = response.obj;
             
/*                    $scope.MstAppearanceTypeGetActiveList.Id === $scope.details.feesConfiguration.AppearanceTypeId;*/
                }
            })
            .error(function (res) {
           
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
 /*   $scope.getApperanceTypeGetByProgPartTermId();*/

  // for list in table
    $scope.getPendingExamFeeConfigurationListGet = function () {
       

        $rootScope.showLoading = true;
    

        $http({
            method: 'POST',
/*            url: 'api/ExamFeeConfigurationHeadMap/getPendingExamFeeConfigurationList',*/
            url: 'api/ExamFeeConfigurationHeadMap/getPendingExamFeeConfigurationList',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.details = response.obj;
          
                    $scope.PendingExamFeeConfigurationList = response.obj.ExamFeeConfigurationHeadMaps;

               /*     $scope.filter.AppearanceTypeId = $scope.details.feesConfiguration.AppearanceTypeId;*/

                    var len = $scope.PendingExamFeeConfigurationList.length;
                    for (var i = 0; i < len; i++) {
                        if (i != 0) {
                            $scope.PendingExamFeeConfigurationList[i].DayRangeTxt = $scope.PendingExamFeeConfigurationList[i - 1].DayRange + " - " + $scope.PendingExamFeeConfigurationList[i].DayRange;
                        }
                        else {
                            $scope.PendingExamFeeConfigurationList[i].DayRangeTxt = "0 - " + $scope.PendingExamFeeConfigurationList[i].DayRange;

                        }
                    }
                    $scope.feeTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.PendingExamFeeConfigurationList
                    });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
 /*   $scope.getPendingExamFeeConfigurationList();*/

    // for edit Exam Fee with condition
    $scope.feeFlag = false;
    $scope.editExamFee = function (data) {
        data.feeFlag = true;
    }
    // for save Exam Fee with condition
    $scope.saveExamFee = function (data) {
        $scope.feeFlag = true;

        if (data.Id !== 0) {
         
            $scope.editExamFeeConfigurationHeadMapEdit(data);

        } else if (data.Id === 0) {
    
            $scope.saveExamFeeConfigurationHeadMapAdd(data);
        }

    }
    // for edit Exam Fee
    $scope.editExamFeeConfigurationHeadMapEdit = function (data) {

        $rootScope.showLoading = true;
        $scope.feeFlag = true;
        /*       $scope.timetable = data;*/

        $http({
            method: 'POST',
            url: 'api/ExamFeeConfigurationHeadMap/ExamFeeConfigurationHeadMapEdit',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {

                    //alert(response.obj);
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.feeFlag = true;
                    $scope.getPendingExamFeeConfigurationListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
              
                alert(res.obj);
            });
    };

    // for save Exam Fee
    $scope.saveExamFeeConfigurationHeadMapAdd = function (data) {

        $rootScope.showLoading = true;
        $scope.feeFlag = true;
        /*     $scope.timetable = data;*/

        $http({
            method: 'POST',
            url: 'api/ExamFeeConfigurationHeadMap/ExamFeeConfigurationHeadMapAdd',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
             /*       alert(response.obj);*/
                    alert(response.obj);
                }
                else {
                    alert(response.obj);
                    $scope.feeFlag = false;
                    $scope.getPendingExamFeeConfigurationListGet();
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    //   // for copy ExamFee config
    //$scope.showCopyExamFeeConfigFlag = false;
    //$scope.addCopyExamFees = function () {
    //    $scope.showCopyExamFeeConfigFlag = true;
    //}

    //$scope.cancelCopyExamFees = function () {
    //    $scope.showCopyExamFeeConfigFlag = false;
    //}

    $scope.noteFlag = false;
    $scope.showNote = function () {
        $scope.noteFlag = true;
    }

});