app.controller('VerifyAssessmentReportCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Verify Assessment Report";
    $scope.ProgInstList = [];
    $scope.ProgInst = {};
    $scope.ShowAfterSubmit = false;
    $scope.ShowLabel = false;
    
    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
               
                $scope.Institute = response.obj[0];  
                              
                $scope.ProgInst.FacultyId = $scope.Institute.Id;
                $scope.ProgInst.InstituteId = $scope.Institute.InstituteId;
                $scope.getPreProgrammeList();
                $scope.getPreProgInstPartTermListByFacultyId();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
                if ($localStorage.PreProgInstData.AcademicYearId != null) {

                    $scope.getgetInstanceNameList();
                }

                $scope.ProgInst.AcademicYearId = $localStorage.PreProgInstData.AcademicYearId;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.FindProgIdPre = function () {
        //debugger;
        if ($scope.ProgInst.ProgrammeId == null || $scope.ProgInst.ProgrammeId == undefined) {
            for (key of Object.keys($scope.InstanceNameList)) {
                if ($scope.InstanceNameList[key].Id == $scope.ProgInst.ProgrammeInstanceId) {
                    ProgId = $scope.InstanceNameList[key].ProgrammeId;
                }
            }
            //$scope.ProgInst.ProgrammeId;
            $scope.ProgInst.ProgrammeId = ProgId;
        }
    };       

    $scope.getgetInstanceNameList = function () {
        //  $scope.ProgInst.FacultyId = $scope.Faculty.Id;
        $scope.ProgInst.InstituteId = $scope.Institute.InstituteId;
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/PreMstProgrammeInstanceListGetbyFacIdAndYearId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code == "201") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    $scope.InstanceNameList = {};
                }
                else {
                    //alert("Change Called");
                  //  console.log("======", response.obj);
                    $scope.InstanceNameList = response.obj;
                    $scope.ProgInst.ProgrammeInstanceId = $localStorage.localObj.ProgrammeInstance;
                    $scope.getBranchListByProgrammeId();
                    $scope.getProgrammePartListByProgrammeId();
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };    

    $scope.getAssessmentReport = function () {

        $localStorage.AssessmentDetails = {};
        $localStorage.AssessmentDetails = $scope.ProgInst;
        $localStorage.AssessmentDetails.ProgrammeInstancePartTermId = $scope.ProgInst.Id.Id; 

        $state.go('VerifyAssessmentReportView');

    };

    $scope.buildTree = function () {
                
        $scope.ProgInst.ProgrammeInstancePartTermId = $scope.ProgInst.Id.Id; 
        if ($scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId == undefined || $scope.ProgInst.AcademicYearId == "" ||
            $scope.ProgInst.ProgrammeInstanceId == null || $scope.ProgInst.ProgrammeInstanceId == undefined || $scope.ProgInst.ProgrammeInstanceId == "" ||
            $scope.ProgInst.ProgrammeInstancePartId == null || $scope.ProgInst.ProgrammeInstancePartId == undefined || $scope.ProgInst.ProgrammeInstancePartId == "" ||
            $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId == undefined || $scope.ProgInst.SpecialisationId == "" ||
            $scope.ProgInst.ProgrammeInstancePartTermId == null || $scope.ProgInst.ProgrammeInstancePartTermId == undefined || $scope.ProgInst.ProgrammeInstancePartTermId == "") {
            alert("Please Select All Fields!");
        }
        else {

            $localStorage.AssessmentreportData = {};
            $http({
                method: 'POST',
                url: 'api/VerifyAssessmentReport/AssessmentReportGetbyProgrammeId',
                data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    }
                    else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        if (response.obj == 'Incomplete') {
                            $scope.ShowLabel = true;
                            $scope.ShowTree = false;
                            $scope.ShowAfterSubmit = false;
                        }
                    }
                    else {
                        $localStorage.AssessmentreportData = response.obj.Item1;
                        $scope.reportOTHERdata = response.obj.Item2;
                        $scope.treeCall1();
                        $scope.ShowTree = true;
                        $scope.ShowAfterSubmit = true;
                        $scope.ShowLabel = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.treeCall1 = function () {

        $scope.treeViewOptions = {
            items: $localStorage.AssessmentreportData,

        };

    };

    $scope.getRemarks = function () {

        var data = { Id: $scope.ProgInst.Id.Id };

        $http({
            method: 'POST',
            url: 'api/VerifyAssessmentReport/GetRemarks',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Remarks = response.obj[0];                
                $scope.ProgInst.AssessmentReportRemark = $scope.Remarks.AssessmentReportRemark;
                $scope.ProgInst.IsApprovedAssessmentReport = $scope.Remarks.IsApprovedAssessmentReport;
                
            })
            .error(function (res) {
                alert(res);
            });
    };
   
    $scope.cancelSelection = function () {
        $scope.ProgInst = {};
        $scope.ShowTree = false;
        $scope.ShowAfterSubmit = false;
        $scope.ShowLabel = false;
    };

    $scope.MstProgrammePartGetByProgrammeIdAndProgInstId = function () {
        if ($localStorage.PreProgInstData.ProgrammeId == null || $localStorage.PreProgInstData.ProgrammeId == undefined) {

            $scope.FindProgIdPre();
            // ProgInst.ProgrammeInstanceId
        }
        else {
            $scope.ProgInst.ProgrammeId = $localStorage.PreProgInstData.ProgrammeId;
        }
        
 
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/MstProgrammePartGetByProgrammeIdAndProgInstId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartList = {};
                }
                else {
                    $scope.ProgPartList = response.obj;
                    if ($localStorage.localObj.ProgrammePartId != null) {
                        $scope.getProgPartTermListByPartId();
                    }
                    $scope.ProgInst.ProgrammePartId = $localStorage.localObj.ProgrammePartId;
                }
            })
            .error(function (res) {
                alert(res);
            });
    }; 

    $scope.getBranchListByProgInstId = function () {
        
        $scope.BranchList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/MstProgrammeBranchListGetByProgInstanceId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.BranchList = response.obj;
                //$scope.TestCountry = {​​​​​
                //}​​​​​;
            })
            .error(function (res) {
                //alert(res);
            });
    };

    $scope.getProgPartTermListByProgInstPartId = function () {
 
        $http({
            method: 'POST',
            url: 'api/ProgrammeInstancePart/ProgrammePartTermGetByProgInstId',
            data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.submit = function () {
        
        if ($scope.ProgInst.AssessmentReportRemark == null || $scope.ProgInst.AssessmentReportRemark == undefined || $scope.ProgInst.AssessmentReportRemark == "" ) {
            alert("Please Enter Faculty Remarks.");
        }
        else if ($scope.ProgInst.IsApprovedAssessmentReport === null || $scope.ProgInst.IsApprovedAssessmentReport === undefined || $scope.ProgInst.IsApprovedAssessmentReport === "" ) {
            alert("Please Select Approved By Faculty.");
        }
        else {

            $scope.VerifyReport = {};
            $scope.VerifyReport.AssessmentReportRemark = $scope.ProgInst.AssessmentReportRemark;
            $scope.VerifyReport.IsApprovedAssessmentReport = $scope.ProgInst.IsApprovedAssessmentReport;
            $scope.VerifyReport.Id = $scope.ProgInst.Id.Id;

            $http({
                method: 'POST',
                url: 'api/VerifyAssessmentReport/VerifyAssessmentReportEdit',
                data: $scope.VerifyReport,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    }
                    else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.ProgInst = {};
                        $scope.ShowTree = false;
                        $scope.ShowAfterSubmit = false;
                        $scope.ShowLabel = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

});

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}

