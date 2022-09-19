app.controller('ApplicationFormDocumentCtrl', function ($scope, $http, $filter, $rootScope, Upload, $localStorage, $state, $cookies, $location, $mdDialog, NgTableParams, $timeout) {

    $scope.ApplicationForm = {};
    $scope.showForm = false;
    $scope.showForm1 = false;
    $scope.SinglePrint = false;
    $scope.BulkPrint = false;

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.Institute = response.obj[0];
                $scope.ApplicationForm.FacultyId = $scope.Institute.Id;
                $scope.ApplicationForm.InstituteId = $scope.Institute.InstituteId;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {

        $http({
            method: 'POST',
            url: 'api/MstProgramInstance/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getInstancePartTerm = function () {

        var data = {
            AcademicYearId: $scope.ApplicationForm.AcademicYearId,
            InstituteId: $scope.Institute.InstituteId
        };

        $http({
            method: 'POST',
            url: 'api/MstProgrammeInstancePartTerm/IncProgrammeInstancePartTermGetByAcadYear',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.InstancePartTermList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.checkRadio = function (data) {

        $scope.ApplicationForm.AFRadio = data;

        if ($scope.ApplicationForm.AFRadio == 'Single') {
            $scope.ApplicationNumber = true;
            $scope.SinglePrint = true;
            $scope.BulkPrint = false;
        }
        else {
            $scope.ApplicationNumber = false;
            $scope.SinglePrint = false;
            $scope.BulkPrint = true;
        }
    };

    $scope.cancelSelection = function () {
        $scope.ApplicationForm = {};
    };

    $scope.showBarcode = function () {
        //  alert("test barocode" + $localStorage.AdmAppId);
        JsBarcode("#barcode2", $scope.ApplicationForm.ApplicationId, {
            format: "code39",
            textAlign: "center",
            textPosition: "bottom",
            font: "cursive",
            fontOptions: "plain",
            fontSize: 40,
            textMargin: 15,
            displayValue: false,
            height: 70,
            width: 2
            //text: "Special"
        });
    };

    $scope.showBulkBarcode = function (data) {
        //  alert("test barocode" + $localStorage.AdmAppId);

        for (var i = 0; i < data.length; i++) {

            JsBarcode("#bulkBarcode2" + data[i].Id, data[i].Id, {
                format: "code39",
                textAlign: "center",
                textPosition: "bottom",
                font: "cursive",
                fontOptions: "plain",
                fontSize: 40,
                textMargin: 15,
                displayValue: false,
                height: 70,
                width: 2
                //text: "Special"
            });
        }
        $scope.offSpinner();

    };

    $scope.getexaminationbodyList = function () {

        var data = {
            //PRN: $scope.ApplicationForm.PRN,
            AdmissionApplicationId: $scope.ApplicationForm.ApplicationId
        };

        $http({
            method: 'POST',
            url: 'api/ProfileForm/getProfileEducationDetailsList',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProfileEduTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getAdmApplicantList = function () {

        var data = {
            //PRN: $scope.ApplicationForm.PRN,
            AdmissionApplicationId: $scope.ApplicationForm.ApplicationId
        };

        $http({
            method: 'POST',
            url: 'api/ProfileForm/AdmApplicantRegistrationGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.ProfileAdmApplicantTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFeePayCourseList = function () {

        $scope.onSpinner();
        if ($scope.ApplicationForm.AFRadio == 'Single') {
            if ($scope.ApplicationForm.AcademicYearId === null || $scope.ApplicationForm.AcademicYearId === undefined || $scope.ApplicationForm.AcademicYearId === "" ||
                $scope.ApplicationForm.ProgrammeInstancePartTermId === null || $scope.ApplicationForm.ProgrammeInstancePartTermId === undefined || $scope.ApplicationForm.ProgrammeInstancePartTermId === "" ||
                $scope.ApplicationForm.ApplicationId === null || $scope.ApplicationForm.ApplicationId === undefined || $scope.ApplicationForm.ApplicationId === "")

                /*($scope.ApplicationForm.ApplicationId === null || $scope.ApplicationForm.ApplicationId === undefined || $scope.ApplicationForm.ApplicationId === "" ||
                $scope.ApplicationForm.PRN === null || $scope.ApplicationForm.PRN === undefined || $scope.ApplicationForm.PRN === ""))*/ {
                alert("Please Select All Fields !");
                $scope.offSpinner();
            }
            else {
                $http({
                    method: 'POST',
                    url: 'api/ProfileForm/getFeePayCourseList',
                    data: {
                        admAppId: $scope.ApplicationForm.ApplicationId
                    },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            $scope.offSpinner();
                        }
                        else {
                            $scope.SinglePrint = true;
                            $scope.BulkPrint = false;
                            $scope.FeePayCourseList1TableParams = new NgTableParams({
                            }, {
                                dataset: response.obj
                            });

                            setTimeout(function () {
                                $scope.offSpinner();
                                $scope.showForm = true;
                                $scope.showBarcode();
                                $scope.getAdmApplicantList();
                                $scope.getexaminationbodyList();
                                $scope.getPostApplicantConfig();
                            }, 3000);



                        }
                    })

                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                        $scope.offSpinner();
                    });
            }
        }

        if ($scope.ApplicationForm.AFRadio == 'BulkInSinglePage') {
            if ($scope.ApplicationForm.AcademicYearId === null || $scope.ApplicationForm.AcademicYearId === undefined || $scope.ApplicationForm.AcademicYearId === "" ||
                $scope.ApplicationForm.ProgrammeInstancePartTermId === null || $scope.ApplicationForm.ProgrammeInstancePartTermId === undefined || $scope.ApplicationForm.ProgrammeInstancePartTermId === "") {
                alert("Please Select Academic Year and Instance Part Term !");
                $scope.offSpinner();
            }
            else {

                $http({
                    method: 'POST',
                    url: 'api/ProfileForm/ApplicantListByInstancePartTerm',
                    data: {
                        InstPartTermId: $scope.ApplicationForm.ProgrammeInstancePartTermId
                    },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            $scope.offSpinner();
                        }
                        else {
                            $scope.SinglePrint = false;
                            $scope.BulkPrint = true;
                            $scope.BulkList = response.obj;
                            //$scope.BlLength = response.obj.length;                            

                            /*if ($cookies.get("InstituteId") == 26) {
                                setTimeout(function () {
                                    //$scope.showForm1 = true;
                                    $scope.showBulkBarcode($scope.BulkList);

                                }, 20000);
                            }

                            if ($cookies.get("InstituteId") == 15 || $cookies.get("InstituteId") == 6) {
                                setTimeout(function () {
                                    //$scope.showForm1 = true;
                                    $scope.showBulkBarcode($scope.BulkList);

                                }, 40000);
                            }

                            if ($cookies.get("InstituteId") == 8 || $cookies.get("InstituteId") == 16 || $cookies.get("InstituteId") == 22) {
                                setTimeout(function () {
                                    //$scope.showForm1 = true;
                                    $scope.showBulkBarcode($scope.BulkList);

                                }, 50000);
                            }

                            if ($cookies.get("InstituteId") == 2 || $cookies.get("InstituteId") == 13 || $cookies.get("InstituteId") == 9 || $cookies.get("InstituteId") == 14) {
                                setTimeout(function () {
                                    //$scope.showForm1 = true;
                                    $scope.showBulkBarcode($scope.BulkList);

                                }, 150000);
                            }                            

                            if ($cookies.get("InstituteId") == 4 || $cookies.get("InstituteId") == 7) {

                                setTimeout(function () {
                                    $scope.showBulkBarcode($scope.BulkList);

                                }, 210000);
                                
                            }*/

                            //$scope.getPostApplicantConfigBulk($scope.BulkList);
                            $scope.getPostApplicantConfigBulk();

                            //console.log($scope.BulkList);
                            //for (key of Object.keys($scope.BulkList)) {
                            //console.log($scope.BulkList[key].Id);
                            //$scope.getPostApplicantConfig($scope.BulkList[key].Id);

                            //}     


                        }
                    })

                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                        $scope.offSpinner();
                    });
            }
        }
    };

    //Function for Get Applicant All Details
    $scope.getPostApplicantConfig = function () {
        $scope.onSpinner();

        if ($scope.ApplicationForm.AFRadio == 'Single') {
            var data = {
                InstPartTermId: $scope.ApplicationForm.ProgrammeInstancePartTermId,
                Id: $scope.ApplicationForm.ApplicationId,
                AcademicYearId: $scope.ApplicationForm.AcademicYearId
            };
        }

        $http({
            method: 'POST',
            url: 'api/ProfileForm/getAdmApplicantRegistrationIdByAdmApplicationId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                    $scope.offSpinner();

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                }
                else {

                    $scope.ApplicantData = response.obj;
                    $scope.CheckAdmDateMessage = $scope.ApplicantData.CheckAdmDateMessage;
                    $scope.ObjPersonal = $scope.ApplicantData.objApplicant;
                    $scope.ObjEducation = $scope.ApplicantData.objEduLst;
                    $scope.ObjSubmittedDoc = $scope.ApplicantData.objSubDocsLst;
                    $scope.ObjAdditional = $scope.ApplicantData.objAdditionalDocsLst;
                    $scope.ObjAddOn = $scope.ApplicantData.objAddOnDocsLst;
                    $scope.ObjAdmAppInfo = $scope.ApplicantData.objAdmApplicationinfo;
                    $scope.ObjEligibilityStatus = $scope.ApplicantData.objFeeLst;
                    $scope.objPreferenceLst = $scope.ApplicantData.objPreferenceLst;
                    $scope.objInstituteLst = $scope.ApplicantData.objInstituteLst;
                    //$scope.objFeePayCourseList = $scope.ApplicantData.objFeePayCourseList;
                    //$scope.AdmApplicantRegistrationList = $scope.ApplicantData.AdmApplicantRegistrationList;
                    //$scope.ProfileEducationDetailsList = $scope.ApplicantData.ProfileEducationDetailsList;
                    $scope.offSpinner();
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                $scope.offSpinner();
            });
    };

    //Function for Get Applicant All Details
    $scope.getPostApplicantConfigBulk = function (data1) {
        //$scope.onSpinner();

        if ($scope.ApplicationForm.AFRadio == 'BulkInSinglePage') {
            var data = {
                InstPartTermId: $scope.ApplicationForm.ProgrammeInstancePartTermId,
                //GetId: data1,
                AcademicYearId: $scope.ApplicationForm.AcademicYearId
            };
        }

        $http({
            method: 'POST',
            url: 'api/ProfileForm/getAdmApplicantRegistrationIdByAdmApplicationIdBulk',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');
                    //$scope.offSpinner();

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
                    //$scope.offSpinner();
                }
                else {

                    $scope.ApplicantData = response.obj;

                    $scope.offSpinner();

                    //$scope.GeneralData = response.obj.Item1;
                    //$scope.PersonalData = response.obj.Item2;
                    /*$scope.ObjPersonal1 = [];
                    $scope.ObjEducation1 = [];
                    $scope.ObjSubmittedDoc1 = [];
                    $scope.ObjAdditional1 = [];
                    $scope.ObjAddOn1 = [];
                    $scope.objPreferenceLst1 = [];
                    $scope.objInstituteLst1 = [];
                    $scope.CheckAdmDateMessage = $scope.ApplicantData.CheckAdmDateMessage;
                    $scope.count = 0;
                    for (var i = 0; i < $scope.ApplicantData.length; i++) {
                        
                       
                        $scope.ObjPersonal1.push($scope.ApplicantData[i].objBulkApplicant[0]);
                        $scope.ObjEducation1.push($scope.ApplicantData[i].objBulkEduLst[0]);
                        $scope.ObjSubmittedDoc1.push($scope.ApplicantData[i].objBulkSubDocsLst[0]);
                        $scope.ObjAdditional1[i] = $scope.ApplicantData[i].objBulkAdditionalDocsLst[0];
                        $scope.ObjAddOn1[i] = $scope.ApplicantData[i].objBulkAddOnDocsLst[0];                        
                        $scope.objPreferenceLst1[i] = $scope.ApplicantData[i].objBulkPreferenceLst[0];
                       
                        $scope.objInstituteLst1[i] = $scope.ApplicantData[i].objBulkInstituteLst[0];                        
                        //$scope.AdmApplicantRegistrationList1[i] = $scope.PersonalData[i].AdmApplicantRegistrationList[0];
                    }*/
                    //for (var i = 0; i < $scope.objPreferenceLst1.length; i++) {


                    //    if ($scope.objPreferenceLst1[i] == undefined) {
                    //        $scope.count++;
                    //    }
                    //    else {
                    //        break;
                    //    }
                    //}
                    //$scope.abc = $scope.objPreferenceLst1.length;


                    /*  $scope.AdmApplicantRegistrationList = $scope.ApplicantData.BulkAdmApplicantRegistrationList;
                    // $scope.ObjAdmAppInfo = $scope.ApplicantData.objAdmApplicationinfo;
                     //$scope.ObjEligibilityStatus = $scope.ApplicantData.objFeeLst;
                    
                     $scope.objFeePayCourseList = $scope.ApplicantData.objFeePayCourseList;
                     AdmApplicantRegistrationList
                     $scope.ProfileEducationDetailsList = $scope.ApplicantData.ProfileEducationDetailsList;*/
                    //$scope.offSpinner();
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
                //$scope.offSpinner();
            });
    };

});