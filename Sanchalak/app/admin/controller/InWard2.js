app.controller('InWardCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Exam Form Generation";

    $rootScope.showLoading = false;


    $scope.cancelInward = function () {
        $scope.filter = {
            ExamMasterId: 0,
        };
    };

    $scope.cancelInward();

// for list in table 1 for schedule in inward
    $scope.getExamFormMasterGetInward = function () {
        $rootScope.showLoading = true;

        var xml = new Object();
        xml.ExamMasterId = $scope.filter.ExamMasterId;

        $scope.ExamFormMasterGetInwardList = [];

        $http({
            method: 'POST',
            url: 'api/ExamFormMaster/ExamFormMasterGetInward',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ExamFormMasterGetInwardList = response.obj;
                  

                    $scope.forminwardTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        dataset: $scope.ExamFormMasterGetInwardList
                    });
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
    //$scope.getExamFormMasterGetInward();


 // for process/generate exam form for schedule

    //$scope.addExamFormMasterAdd = function (data) {

    //    $rootScope.showLoading = true;
    //    $scope.ExamFormMasterPartTermStatusGetList.ExamMasterId = $scope.filter.ExamMasterId;
    //    $scope.ExamFormMasterPartTermStatusGetList.FacultyExamMapId = data.FacultyExamMapId;

    //    $http({
    //        method: 'POST',
    //        url: 'api/ExamFormMaster/ExamFormMasterAdd',
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
    //                $scope.getExamFormMasterPartTermStatusGet();
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.showLoading = false;
    //            alert(res.obj);
    //        });
    //};

// for courses  for table 2

    $scope.showCourseFlag = false;

    $scope.attachCourses = function (data) {

        $scope.showCourseFlag = true;
        $scope.selectedSchedule = data;
        $scope.getExamFormMasterGetInwardByFacutyExamMapId($scope.selectedSchedule);
        $scope.filter.FacultyExamMapId = data.FacultyExamMapId;
    }

    $scope.cancelCourses = function () {
        $scope.showCourseFlag = false;
        //$scope.selectedSchedule = {

        //};
        $scope.filter = {
            AcademicYearId: 0,
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0,
            ProgrammePartTermId: 0,
            BranchId: 0,
            ProgInstancePartTermId:0
        };
    };

 /*   $scope.cancelCourses();*/

    // for show view 2 based on dropdown of schedule selection 
    $scope.attachDetailBasedonSchedules = function (data) {
        $scope.showCourseFlag = true;
        $scope.getExamFormMasterGetInwardByFacutyExamMapId(data);
    }

    // for get only active courses table 2  with inward
    $scope.getExamFormMasterGetInwardByFacutyExamMapId = function (data) {

        $rootScope.showLoading = true;

        var xml = new Object();
        $scope.selectedSchedule = data;
        xml.FacultyExamMapId = data.FacultyExamMapId;
     
     

        $http({
            method: 'POST',
            url: 'api/ExamFormMaster/ExamFormMasterGetInwardByFacutyExamMapId',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.ExamFormMasterGetInwardByFacutyExamMapIdList = response.obj;
            
                 

                    $scope.coursesinwardTableParams = new NgTableParams({
                    }, {
                        dataset: $scope.ExamFormMasterGetInwardByFacutyExamMapIdList
                    });
                }

            })
            .error(function (res) {

                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };

    // for save
    $scope.updateUpdateIsExamFeesPaidByPRN = function () {
        $rootScope.showLoading = true;
       
        $http({
            method: 'POST',
            url: 'api/ExamFormMaster/updateIsExamFeesPaidByPRN',
            data: $scope.filter,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
                    alert(response.obj);

                }
                else {
                    alert(response.obj);
                   
                 /*   $scope.getExamCenterListGet();*/
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };



});