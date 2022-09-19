app.controller('PostConfigurationCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
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
            url: 'api/MstFaculty/MstFacultyGetbyId',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0];
                $scope.PostProgInst.FacultyId = $scope.Faculty.Id;
                //$scope.getIncProgPartTermByFacIdList();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getIncProgPartTermByFacIdList = function () {
        
        //var InstituteId = { InstituteId: $scope.Faculty.InstituteId, AcademicYearId: $scope.PostProgInst.AcademicYearId };
        $scope.PostProgInst.InstituteId = $scope.Faculty.InstituteId;
        $scope.PostProgInst.AcademicYearId = $scope.PostProgInst.AcademicYearId;
        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/PostProgPartTermGetByFacIdandYearId',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgPartTermByFacIdList = response.obj;
                $scope.offSpinner();
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

    //Function for get No. of Count for Sent SMS and Email
    $rootScope.GetApplicantsCountforFacultyVerification = function () {
        
        $http({
            method: 'POST',
            url: 'api/PostConfigurationAdmission/PostApplicantsCountforFacultyVerification',
            data: { ProgrammeInstancePartTermId: $scope.PostProgInst.ProgrammeInstancePartTermId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.GetApplicantsCountforFacultyVerification = response.obj[0];
                $scope.ModelFacultyCount = {};
                $scope.ModelFacultyCount.TotalCountforFaculty = $scope.GetApplicantsCountforFacultyVerification.TotalCountforFaculty;
                $scope.ModelFacultyCount.ProApprovedCountforFaculty = $scope.GetApplicantsCountforFacultyVerification.ProApprovedCountforFaculty;
                $scope.ModelFacultyCount.NotApprovedCountforFaculty = $scope.GetApplicantsCountforFacultyVerification.NotApprovedCountforFaculty;
                $scope.ModelFacultyCount.PendingCountforFaculty = $scope.GetApplicantsCountforFacultyVerification.PendingCountforFaculty;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for get Applicant list by Programme Instance PartTerm Id
    $scope.getApplicantListByProgPartTerm = function (InstPartTermId) {

        if ($localStorage.BacktoPostPage.Flag == true) {
            if ($localStorage.BacktoPostPage.SubmitFlag == true) {
                $localStorage.VerificationData[$localStorage.BacktoPostPage.Index1].EligibilityStatus = $localStorage.BacktoPostPage.FacultyStatus;
                $localStorage.VerificationData[$localStorage.BacktoPostPage.Index1].AdminRemarkByFaculty = $localStorage.BacktoPostPage.FacultyRemarks;
                $scope.VerificationList = $localStorage.VerificationData;

                if ($.fn.dataTable.isDataTable('#VerificationListTable')) {
                    $('#VerificationListTable').dataTable().fnClearTable();
                    $('#VerificationListTable').DataTable().destroy();
                }
                $(document).ready(function () {
                    $('#VerificationListTable').DataTable({
                        "bPaginate": true,
                        "paging": true,
                        "ordering": false,
                        "bLengthChange": true,
                        "info": false,
                        "searching": true,
                    });
                });

                $localStorage.BacktoPostPage.Flag = false;
                $localStorage.BacktoPostPage.SubmitFlag = false;
            }
            else {
                $scope.VerificationList = $localStorage.VerificationData;
                if ($.fn.dataTable.isDataTable('#VerificationListTable')) {
                    $('#VerificationListTable').dataTable().fnClearTable();
                    $('#VerificationListTable').DataTable().destroy();
                }
                $(document).ready(function () {
                    $('#VerificationListTable').DataTable({
                        "bPaginate": true,
                        "paging": true,
                        "ordering": false,
                        "bLengthChange": true,
                        "info": false,
                        "searching": true,
                    });
                });

                $localStorage.BacktoPostPage.Flag = false;
            }
        }
        else {


            $localStorage.EligibilityStatus = $scope.PostProgInst.EligibilityStatus;

            $scope.checkDataExists = false;

            $localStorage.InstancePartTermId = InstPartTermId;
            //$localStorage.InstPartTermName = InstancePartTermName;
            //alert("Id"+$localStorage.InstancePartTermId);

            if (InstPartTermId == null || InstPartTermId == "" || InstPartTermId === undefined) {
                alert("Please select Programme...!");
            }
            else if ($scope.PostProgInst.EligibilityStatus == null || $scope.PostProgInst.EligibilityStatus == "" || $scope.PostProgInst.EligibilityStatus === undefined) {
                alert("Please select Status...!");
            }
            else {
                $scope.onSpinner();

                $http({
                    method: 'POST',
                    url: 'api/PostConfigurationAdmission/PostApplicantListByIncProgrammePartTermId',
                    data: $scope.PostProgInst,
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        // $scope.ApplicantByProgPartTermList = response.obj;

                        $scope.PostProgInstPartTermTableparam = new NgTableParams({

                            page: 1,
                            Count: 100
                        }, { dataset: response.obj });
                        //console.log($scope.PostProgInstPartTermTableparam.data);
                        $scope.VerificationList = response.obj;
                        $scope.ShowTable = true;

                        if ($.fn.dataTable.isDataTable('#VerificationListTable')) {
                            $('#VerificationListTable').dataTable().fnClearTable();
                            $('#VerificationListTable').DataTable().destroy();
                        }
                        $(document).ready(function () {
                            $('#VerificationListTable').DataTable({
                                "bPaginate": true,
                                "paging": true,
                                "ordering": false,
                                "bLengthChange": true,
                                "info": false,
                                "searching": true,
                            });
                        });

                        $scope.offSpinner();
                        if (response.obj.length == 0) {
                            $scope.checkDataExists = true;
                        }
                        $rootScope.GetApplicantsCountforFacultyVerification();
                    })
                    .error(function (res) {
                        alert(res);
                    });
            }
        }
    };

    //Function for Check Applicant already approved for other Programme or not
    $scope.VerifyApplicant = function (AppId, InstPartTermName, ApplicantRegistrationId, AcademicYearId) {

        $localStorage.VerificationAppId = AppId;
        $localStorage.InstancePartTermName = InstPartTermName;
        $localStorage.VerificationAppRegId = ApplicantRegistrationId;
        $localStorage.AcademicYearId = AcademicYearId;
        $localStorage.VerificationData = $scope.VerificationList;
        $state.go('PostApplicantVerification');

        //$http({
        //    method: 'POST',
        //    url: 'api/PostApplicantVerification/UpdateFacultyMainSubmit',
        //    data: {
        //        Id: AppId,
        //        ApplicantRegId: ApplicantRegistrationId,
        //        ProgrammeInstancePartTermId: $localStorage.InstancePartTermId
        //    },
        //    headers: { "Content-Type": 'application/json' }
        //})
        //    .success(function (response) {
        //        $rootScope.showLoading = false;

        //        if (response.response_code != "200") {
        //            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
        //        }
        //        else {
        //            if (response.obj.includes('Student Application is already Approved for the')) {
        //                alert(response.obj);
        //            }
        //            else {
        //                $localStorage.VerificationAppId = AppId;
        //                $localStorage.InstancePartTermName = InstPartTermName;
        //                $localStorage.VerificationAppRegId = ApplicantRegistrationId;
        //                $state.go('PostApplicantVerification');
        //            }
        //        }
        //    })
        //    .error(function (res) {
        //        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
        //    });



    };

    $scope.cancelPostProgInsPartTermList = function () {
        $scope.PostProgInst = {};
        $scope.showFormFlag = false;
    };

    if ($localStorage.BacktoPostPage.Flag == true) {
        $scope.onSpinner();
        //$localStorage.BacktoPostPage = {};
        //$localStorage.BacktoPostPage.Flag = false;
        $scope.PostProgInst.AcademicYearId = $localStorage.BacktoPostPage.AcademicYearId;
        $scope.PostProgInst.ProgrammeInstancePartTermId = $localStorage.BacktoPostPage.ProgPTID;
        $scope.PostProgInst.EligibilityStatus = $localStorage.BacktoPostPage.EligibilityStatus;
        $scope.getApplicantListByProgPartTerm($scope.PostProgInst.ProgrammeInstancePartTermId);
        $rootScope.GetApplicantsCountforFacultyVerification($scope.PostProgInst.ProgrammeInstancePartTermId);
       
    }
    else {
        $localStorage.BacktoPostPage = {};
    }


    //Start - Mohini's code Added on 18-May-2022

    $scope.getAcademicList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            data: $scope.PostProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;
                
                if ($localStorage.BacktoPostPage.AcademicYearId != null) {

                    $scope.getIncProgPartTermByFacIdList();
                }

                $scope.PostProgInst.AcademicYearId = $localStorage.BacktoPostPage.AcademicYearId;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //End - Mohini's code Added on 18-May-2022

});



