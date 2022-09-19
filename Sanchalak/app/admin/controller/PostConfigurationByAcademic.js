app.controller('PostConfigurationByAcademicCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Part Term Detail";
   
    $scope.PostProgInst = {};
    //$scope.PostProgInstPartTermTableparam = new NgTableParams(
    //    {}, {
    //    dataset: $scope.getProgInstList
    //});

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    //Function for expand columns in row click
    $scope.expand_row = function (id) {
        let element = document.getElementById('expand' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + id).innerHTML = "+"
            element.add("collapse")
        }
    }

    $scope.resetProgInstPartTerm = function () {
        $scope.PostProgInst = {};
    };

    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
             
                //$scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                //$scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getPostInstitutebyFaculty = function () {

        $scope.AcadList = {};
        $scope.ProgPartTermByFacIdList = {};
        $scope.facultyeligibilitystatus = {};

        var FacultyId = { FacultyId: $scope.PostProgInst.Id };

        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/PostInstituteGetbyFaculty',
            data: FacultyId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermByInstituteList = response.obj;
                $scope.getAcademicList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgPartTermByFacIdList = function () {

        $scope.ProgPartTermByFacIdList = {};

        $scope.InstituteId = $scope.PostProgInst.InstituteId;
        $scope.AcademicYearId = $scope.PostProgInst.AcademicYearId;

        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFaculty',
            data: {
                InstituteId: $scope.InstituteId,
                FacultyId: $scope.PostProgInst.Id,
                AcademicYearId: $scope.AcademicYearId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.ProgPartTermByFacIdList = {};
                }
                else {
                    $scope.ProgPartTermByFacIdList = response.obj;
                }
            })
            
            .error(function (res) {
                alert(res);
            });
    };

    //Function for gwt FullProgNameByProgPTID
    $scope.PostGetFullProgNameByProgPTID = function () {
       
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PostGetFullProgNameByProgPTID',
            data: { IncProgInstancePartTermId: $scope.PostProgInst.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.GetFullProgNameByProgPTID = response.obj[0];

                $localStorage.PostVerify = {};
                $localStorage.PostVerify.ProgrammeName = $scope.GetFullProgNameByProgPTID.ProgrammeName;
                $localStorage.PostVerify.BranchName = $scope.GetFullProgNameByProgPTID.BranchName;
                $localStorage.PostVerify.AcademicYearCode = $scope.GetFullProgNameByProgPTID.AcademicYearCode;
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for get No. of Count for Academic Verification
    $rootScope.PostCountsforAcademicVerification = function () {

        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/PostCountsforAcademicVerification',
            data: { ProgrammeInstancePartTermId: $scope.PostProgInst.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.GetCountsforAcademicVerification = response.obj[0];
                $scope.ModelAcademicCount = {};
                $scope.ModelAcademicCount.TotalCount = $scope.GetCountsforAcademicVerification.TotalCount;
                $scope.ModelAcademicCount.EligibleCountByFaculty = $scope.GetCountsforAcademicVerification.EligibleCountByFaculty;
                $scope.ModelAcademicCount.EligibleCountByAcademic = $scope.GetCountsforAcademicVerification.EligibleCountByAcademic;
                $scope.ModelAcademicCount.AdmFeePaidCount = $scope.GetCountsforAcademicVerification.AdmFeePaidCount;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for get Applicant list by Programme Instance PartTerm Id
    $scope.getApplicantListByProgPartTerm = function (InstituteId, InstPartTermId, AcademicYearId) {
        
        if ($localStorage.BacktoPostPageAcademic.Flag == true) {
            $scope.onSpinner();
            if ($localStorage.BacktoPostPageAcademic.SubmitFlag == true) {
                $localStorage.AcademicVerificationData[$localStorage.BacktoPostPageAcademic.Index1].ApprovedByAcademics = $localStorage.BacktoPostPageAcademic.ApprovedByAcademics;
                $localStorage.AcademicVerificationData[$localStorage.BacktoPostPageAcademic.Index1].EligibilityByAcademics = $localStorage.BacktoPostPageAcademic.AcademicStatus;
                $localStorage.AcademicVerificationData[$localStorage.BacktoPostPageAcademic.Index1].AdminRemarkByAcademics = $localStorage.BacktoPostPageAcademic.AdminRemarkByAcademics;
                $scope.AcademicVerificationList = $localStorage.AcademicVerificationData;

                if ($.fn.dataTable.isDataTable('#AcademicVerificationListTable')) {
                    $('#AcademicVerificationListTable').dataTable().fnClearTable();
                    $('#AcademicVerificationListTable').DataTable().destroy();
                }
                $(document).ready(function () {
                    $('#AcademicVerificationListTable').DataTable({
                        "bPaginate": true,
                        "paging": true,
                        "ordering": false,
                        "bLengthChange": true,
                        "info": false,
                        "searching": true,
                    });
                });

                $localStorage.BacktoPostPageAcademic.Flag = false;
                $localStorage.BacktoPostPageAcademic.SubmitFlag = false;
            }

            else {
                $scope.AcademicVerificationList = $localStorage.AcademicVerificationData;
                if ($.fn.dataTable.isDataTable('#AcademicVerificationListTable')) {
                    $('#AcademicVerificationListTable').dataTable().fnClearTable();
                    $('#AcademicVerificationListTable').DataTable().destroy();
                }
                $(document).ready(function () {
                    $('#AcademicVerificationListTable').DataTable({
                        "bPaginate": true,
                        "paging": true,
                        "ordering": false,
                        "bLengthChange": true,
                        "info": false,
                        "searching": true,
                    });
                });

                $localStorage.BacktoPostPageAcademic.Flag = false;
            }
            //$scope.offSpinner();
        }
        else {

            $scope.checkDataExists = false;
            $localStorage.InstituteId = InstituteId;
            $localStorage.InstancePartTermId = InstPartTermId;
            $localStorage.AcademicYearId = AcademicYearId;
            $localStorage.FacultyId = $scope.PostProgInst.Id;
            $localStorage.FacultyEligibilityStatus = $scope.PostProgInst.FacultyEligibilityStatus;
            //$localStorage.InstPartTermName = InstancePartTermName;
            //alert("Id"+$localStorage.InstancePartTermId);

            if ($scope.PostProgInst.Id == null || $scope.PostProgInst.Id == "" || $scope.PostProgInst.Id === undefined) {
                alert("Please select Faculty...!");
            }
            else if (InstituteId == null || InstituteId == "" || InstituteId === undefined) {
                alert("Please select Institute...!");
            }
            else if (InstPartTermId == null || InstPartTermId == "" || InstPartTermId === undefined) {
                alert("Please select Programme...!");
            }
            else if ($scope.PostProgInst.FacultyEligibilityStatus == null || $scope.PostProgInst.FacultyEligibilityStatus == "" || $scope.PostProgInst.FacultyEligibilityStatus === undefined) {
                alert("Please select Faculty Approval...!");
            }
            else {
                $scope.onSpinner();
                $http({
                    method: 'POST',
                    url: 'api/PostConfigurationAdmission/PostApprovedApplicantListByIncPartTerm',
                    data: $scope.PostProgInst,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        // $scope.ApplicantByProgPartTermList = response.obj;
                        $scope.PostProgInstPartTermTableparam = new NgTableParams({

                            page: 1,
                            Count: 100
                        }, { dataset: response.obj });
                        $scope.AcademicVerificationList = response.obj;
                        $rootScope.PostCountsforAcademicVerification();
                        $scope.offSpinner();
                        if (response.obj.length == 0) {
                            $scope.checkDataExists = true;
                        }

                        if ($.fn.dataTable.isDataTable('#AcademicVerificationListTable')) {
                            $('#AcademicVerificationListTable').dataTable().fnClearTable();
                            $('#AcademicVerificationListTable').DataTable().destroy();
                        }
                        $(document).ready(function () {
                            $('#AcademicVerificationListTable').DataTable({
                                "bPaginate": true,
                                "paging": true,
                                "ordering": false,
                                "bLengthChange": true,
                                "info": false,
                                "searching": true,
                            });
                        });

                    })
                    .error(function (res) {
                        alert(res);
                    });
            }
        }
    };

    $scope.VerifyApplicantByAcademic = function (AppId, InstPartTermName, ApplicantRegistrationId) {
        $localStorage.VerificationAppId = AppId;
        $localStorage.InstancePartTermName = InstPartTermName;
        $localStorage.VerificationAppRegId = ApplicantRegistrationId;
        $localStorage.AcademicVerificationData = $scope.AcademicVerificationList;
        $state.go('PostApplicantVerificationByAcademic');
    };

    $scope.cancelPostProgInsPartTermList = function () {
        $scope.PostProgInst = {};
        $scope.showFormFlag = false;
    };
    
    if ($localStorage.BacktoPostPageAcademic.Flag == true) {
        
        $scope.onSpinner();
        //$localStorage.BacktoPostPageAcademic = {};
        //$localStorage.BacktoPostPageAcademic.Flag = false;
        $scope.PostProgInst.Id = $localStorage.BacktoPostPageAcademic.FacultyId;
        $scope.PostProgInst.InstituteId = $localStorage.BacktoPostPageAcademic.InstituteId;
        $scope.PostProgInst.ProgrammeInstancePartTermId = $localStorage.BacktoPostPageAcademic.ProgPTID;
        $scope.PostProgInst.AcademicYearId = $localStorage.BacktoPostPageAcademic.AcademicYearId;
        $scope.PostProgInst.FacultyEligibilityStatus = $localStorage.BacktoPostPageAcademic.FacultyEligibilityStatus;
        
        //$scope.getFacultyById();
        $scope.getPostInstitutebyFaculty();
        $scope.getIncProgPartTermByFacIdList();
        $scope.getApplicantListByProgPartTerm($scope.PostProgInst.InstituteId, $scope.PostProgInst.ProgrammeInstancePartTermId);
        $rootScope.PostCountsforAcademicVerification($scope.PostProgInst.ProgrammeInstancePartTermId);
        //$scope.offSpinner();
    }
    else {
        $localStorage.BacktoPostPageAcademic = {};
    }

    //Start - Mohini's code Added on 21-May-2022

    $scope.getAcademicList = function () {

        $scope.AcadList = {};
        $scope.ProgPartTermByFacIdList = {};
        $scope.facultyeligibilitystatus = {};

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
                $scope.offSpinner();
                //if ($localStorage.BacktoPostPage.AcademicYearId != null) {

                //    $scope.getIncProgPartTermByFacIdList();
                //}

                //$scope.PostProgInst.AcademicYearId = $localStorage.BacktoPostPage.AcademicYearId;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //End - Mohini's code Added on 21-May-2022

});



