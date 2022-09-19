app.controller('CopyExamFeeConfigurationCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams, $window) {

    $rootScope.pageTitle = "Manage Copy Exam Fee Configuration";

    $rootScope.showLoading = false;

    $scope.cancelMstCopyExamFeesConfiguration = function () {
        $scope.copyfee = {
            ExamMasterId: 0,
            FacultyExamMapId: 0,
            ProgrammeId: 0,
            ProgrammePartTermId:0
        };
    };

    $scope.cancelMstCopyExamFeesConfiguration();

    // for list in table

    $scope.getGetProgrammePartListForExamFeeConfig = function () {
        
        $rootScope.showLoading = true;
      
        var xml = new Object();
      
        xml.FacultyExamMapId = $scope.copyfee.FacultyExamMapId;
        xml.ExamMasterId = $scope.copyfee.ExamMasterId;

        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTerm/GetProgrammePartListForExamFeeConfig',
            data: xml,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    alert(response.obj);
                }
                else {
                    $scope.GetProgrammePartListForExamFeeConfigList = response.obj;
                    $scope.details = response.obj.programmePartTermList;
                    //alert(response.obj);
                    $scope.copyfeeTableParams = new NgTableParams({
                        count: 1000
                    }, {
                        //dataset: $scope.GetProgrammePartListForExamFeeConfigList
                        dataset: $scope.details
                    });
                    //alert($scope.GetProgrammePartListForExamFeeConfigList);
                }
            })
            .error(function (res) {
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
 /*   $scope.getGetProgrammePartListForExamFeeConfig();*/

    // for generate button
    $scope.copyCopyExamFeesConfiguratonHeadMap = function () {

        $rootScope.showLoading = true;
   
        $scope.GetProgrammePartListForExamFeeConfigList.FacultyExamMapId = $scope.copyfee.FacultyExamMapId;
        $scope.GetProgrammePartListForExamFeeConfigList.ExamMasterId = $scope.copyfee.ExamMasterId;
        $scope.GetProgrammePartListForExamFeeConfigList.ProgrammeId = $scope.copyfee.ProgrammeId;
        $scope.GetProgrammePartListForExamFeeConfigList.ProgrammePartTermIdFrom = $scope.copyfee.ProgrammePartTermId;
        

        $http({
            method: 'POST',
            /*        url: 'api/CourseScheduleMap/MstCouseScheduleMapListAdd',*/
            url: 'api/ExamFeeConfigurationHeadMap/CopyExamFeesConfiguratonHeadMap',
            data: $scope.GetProgrammePartListForExamFeeConfigList,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
       
                $rootScope.showLoading = false;

                if (response.response_code !== "200") {
               
                    alert(response.obj);
                }
                else {
                
                    alert(response.obj);
                    $scope.getGetProgrammePartListForExamFeeConfig();
                }
            })
            .error(function (res) {
         
                $rootScope.showLoading = false;
                alert(res.obj);
            });
    };
});