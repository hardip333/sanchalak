
app.controller('PostApplicantVerificationCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Post Applicant Verification";
    //$scope.PostApplicantConfig = {};
    var verifymsg = "";
    var submitmsg = "";
    var submitmsgEligible = "";
    $scope.EligibilityStatus = ""; 
    $scope.remarks = "";
    $scope.facremarksObj = { remarks: "" }
    $scope.FeeCategoryPartTermMapId = 0;
    $scope.InstituteId = 0;
    //$scope.PreferenceGroupId = 0;
    //$scope.DestProgID = 0;
    //$scope.RemarkByFaculty = "";
    $scope.remarksObj = { dualremarks: ""}
    $scope.ApplicantData = {};
    $scope.ProgrammeInstancePartTermId = 0;
    $scope.docName = "";
    $scope.DocTableIndex = -1;
    $scope.PendingDocListFlag = "";
    $scope.checkDestIDFlag = false;
    $scope.checkReAdmissionFlag = false;
    $scope.ViewAdmittedCourseFlag = false;

    $scope.PostApplicantConfigTableparam = new NgTableParams(
        {}, {
        dataset: $scope.PostApplicantData
    });

    $scope.testclick = function (abc) {

        //debugger
        if (abc == "DOBDoc") {

            if ($scope.ObjPersonal.DOBDoc != undefined && $scope.ObjPersonal.DOBDoc != null &&
                $scope.ObjPersonal.DOBDoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.DOBDoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "PhotoIdDoc") {

            if ($scope.ObjPersonal.PhotoIdDoc != undefined && $scope.ObjPersonal.PhotoIdDoc != null &&
                $scope.ObjPersonal.PhotoIdDoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.PhotoIdDoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "AadharDoc") {

            if ($scope.ObjPersonal.AadharDoc != undefined && $scope.ObjPersonal.AadharDoc != null &&
                $scope.ObjPersonal.AadharDoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.AadharDoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "SocialCategoryDoc") {

            if ($scope.ObjPersonal.SocialCategoryDoc != undefined && $scope.ObjPersonal.SocialCategoryDoc != null &&
                $scope.ObjPersonal.SocialCategoryDoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.SocialCategoryDoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "ReservationCategoryDoc") {

            if ($scope.ObjPersonal.ReservationCategoryDoc != undefined && $scope.ObjPersonal.ReservationCategoryDoc != null &&
                $scope.ObjPersonal.ReservationCategoryDoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.ReservationCategoryDoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "NCLCerti") {

            if ($scope.ObjPersonal.NCLCerti != undefined && $scope.ObjPersonal.NCLCerti != null &&
                $scope.ObjPersonal.NCLCerti != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.NCLCerti);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "EWSDoc") {

            if ($scope.ObjPersonal.EWSDoc != undefined && $scope.ObjPersonal.EWSDoc != null &&
                $scope.ObjPersonal.EWSDoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.EWSDoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "EWS_IADoc") {

            if ($scope.ObjPersonal.EWS_IADoc != undefined && $scope.ObjPersonal.EWS_IADoc != null &&
                $scope.ObjPersonal.EWS_IADoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.EWS_IADoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "PCDoc") {

            if ($scope.ObjPersonal.PCDoc != undefined && $scope.ObjPersonal.PCDoc != null &&
                $scope.ObjPersonal.PCDoc != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.PCDoc);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "ApplicantPhoto") {

            if ($scope.ObjPersonal.ApplicantPhoto != undefined && $scope.ObjPersonal.ApplicantPhoto != null &&
                $scope.ObjPersonal.ApplicantPhoto != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.ApplicantPhoto);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
        if (abc == "ApplicantSignature") {

            if ($scope.ObjPersonal.ApplicantSignature != undefined && $scope.ObjPersonal.ApplicantSignature != null &&
                $scope.ObjPersonal.ApplicantSignature != "") {

                $("#imageID").attr('src', $scope.ObjPersonal.ApplicantSignature);
            }
            else {

                $("#imageID").attr('src', "No File");
            }
        }
    }

    $scope.testclickEdu = function (i, ObjEducation) {
        //debugger
        $scope.ObjEdu = ObjEducation;

        if ($scope.ObjEdu[i].AttachDocument != undefined && $scope.ObjEdu[i].AttachDocument != null &&
            $scope.ObjEdu[i].AttachDocument != "") {

            $("#imageID").attr('src', $scope.ObjEdu[i].AttachDocument);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }

    $scope.testclickReq = function (i, ObjSubmittedDoc) {
        //debugger
        $scope.sdocument = ObjSubmittedDoc;

        if ($scope.sdocument[i].FileNameofDocumnetBySystem != undefined && $scope.sdocument[i].FileNameofDocumnetBySystem != null &&
            $scope.sdocument[i].FileNameofDocumnetBySystem != "") {

            $("#imageID").attr('src', $scope.sdocument[i].FileNameofDocumnetBySystem);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickAdd = function (i, ObjAdditional) {
        //debugger
        $scope.additional = ObjAdditional;

        if ($scope.additional[i].DocFileName != undefined && $scope.additional[i].DocFileName != null &&
            $scope.additional[i].DocFileName != "") {

            $("#imageID").attr('src', $scope.additional[i].DocFileName);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.resetPostApplicantConfig = function () {
        $scope.PostApplicantData = {};
    };


    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


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

    $scope.getEmailByKeyName = function (data) {
        return $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/GenericConfigurationGetByKeyName',
            data: { KeyName: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

    ////Funcion for Get Required Document List By ProgrammeId
    //$scope.getRequiredDocListByProgramme = function () {

    //    $scope.InstPartTermId = $localStorage.InstancePartTermId;

    //    $http({
    //        method: 'Post',
    //        url: 'api/VerifyApplicantProfileForm/GetRequiredDocListByProgramme',
    //        data: { ProgInstPartTermId: $scope.InstPartTermId },
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {

    //            $scope.RequiredDocList = response.obj;
     
    //        })
    //        .error(function (res) {
    //            alert(res);
    //        });
    //};


    //Funcion for Get Institute List

    $scope.getInstituteListByIncPartTermId = function () {

        $scope.InstPartTermId = $localStorage.InstancePartTermId;

        $http({
            method: 'Post',
            url: 'api/VerifyApplicantProfileForm/PostInstituteGetByIncPartTerm',
            data: {
                ProgrammeInstancePartTermId: $scope.InstPartTermId,
                ApplicationId: $localStorage.VerificationAppId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
               
                $scope.InstituteList = response.obj;
                //$scope.instituteId = $scope.InstituteList[0].InstituteId;

                console.log("=====", $scope.InstituteList);
                //alert("=====", $scope.InstituteList[0].InstituteId);
                //debugger
                for (let i = 0; i < $scope.InstituteList.length; i++) {
                    if ($scope.ApplicantData.objAdmApplicationinfo.AdmittedInstituteId == $scope.InstituteList[i].InstituteId) {
                        $scope.InstituteId = $scope.ApplicantData.objAdmApplicationinfo.AdmittedInstituteId;
                    }
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for Get Group Preferenece List
    $scope.getMstPreferenceGroup = function () {

        $scope.checkFeeFlag = false;
        var data = new Object();
        data.ProgInstPartTermId = $localStorage.InstancePartTermId;

        $http({
            method: 'POST',
            url: 'api/VerifyApplicantProfileForm/PostGetGroupNameListByProgInstPTID',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.GroupPrefList = response.obj;

                    for (let i = 0; i < $scope.GroupPrefList.length; i++) {

                        if ($scope.ApplicantData.objAdmApplicationinfo.PreferenceGroupId == $scope.GroupPrefList[i].PreferenceId) {
                            $scope.PreferenceGroupId = $scope.ApplicantData.objAdmApplicationinfo.PreferenceGroupId;
                        }
                    }
                    if (response.obj.length == 0) {
                        $scope.checkFeeFlag = true;
                        $scope.getFeeCategoryByDestPTID();
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Function for Get DestinationProgPTID by Preference ID
    $scope.PostGetDestinationProgPTID = function (PreferenceGroupId) {
        $scope.PreferenceGroupId = PreferenceGroupId;
        $http({
            method: 'POST',
            url: 'api/VerifyApplicantProfileForm/PostGetDestinationProgPTID',
            data: {
                Id: PreferenceGroupId,
                ProgInstPartTermId: $scope.InstPartTermId
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.DestProgList = response.obj;

                    for (let i = 0; i < $scope.DestProgList.length; i++) {

                        if ($scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId == $scope.DestProgList[i].DestinationIncProgInstPartTermId) {
                            $scope.DestProgID = $scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId;
                        }
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Function for Get Applicant All Details
    $scope.getPostApplicantConfig = function () {

        $scope.AcademicYearId = $localStorage.AcademicYearId;
        //alert("Check AcademicYearId : " + $scope.AcademicYearId);

        $scope.InstPartTerm = $localStorage.InstancePartTermName;
        //alert("Check Part : " + $scope.InstPartTerm);

        $scope.InstPartTermId = $localStorage.InstancePartTermId;
        //alert("Check InstPartTermId : " + $scope.InstPartTermId);

        $scope.ApplicantRegId = $localStorage.VerificationAppRegId;
        //alert("Check RegId : " + $localStorage.VerificationAppRegId);

        $scope.localEligibilityStatus = $localStorage.EligibilityStatus;

        $http({
            method: 'POST',
            url: 'api/VerifyApplicantProfileForm/getAdmApplicantRegistrationIdByAdmApplicationId',
            data: {
                InstPartTermId: $scope.InstPartTermId,
                AcademicYearId: $scope.AcademicYearId,
                Id: $localStorage.VerificationAppId,
                EligibilityStatus: $scope.localEligibilityStatus
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.broadcast('dialog', "Error", "alert", response.obj);
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

                    for (let i = 0; i < $scope.ObjEligibilityStatus.length; i++) {
                        //if ($scope.ApplicantData.objAdmApplicationinfo.EligibilityStatus == $scope.ObjEligibilityStatus[i].EligibilityStatus) {
                        //    $scope.EligibilityStatus = $scope.ApplicantData.objAdmApplicationinfo.EligibilityStatus;
                        //}
                        if ($scope.ObjAdmAppInfo.FeeCategoryPartTermMapId == $scope.ObjEligibilityStatus[i].FeeCategoryPartTermMapId) {
                            $scope.FeeCategoryPartTermMapId = $scope.ObjAdmAppInfo.FeeCategoryPartTermMapId;
                        }
                        if ($scope.ObjEligibilityStatus[i].ApplicationIDStatus == "True") {
                            $scope.EligibilityStatus = $scope.ObjEligibilityStatus[i].EligibilityStatus;
                            $scope.facremarksObj.remarks = $scope.ObjEligibilityStatus[i].AdminRemarkByFaculty
                        }
                    }
                    //For Selected IsEWS Radio
                    if ($scope.ObjPersonal.IsEWS == 1 || $scope.ObjPersonal.IsEWS == "True") {
                        $scope.ObjPersonal.IsEWS = "True";
                    }
                    else {
                        $scope.ObjPersonal.IsEWS = "False";
                    }

                    //for (var i = 0; i < $scope.ObjEligibilityStatus.length; i++) {
                    //    if ($scope.ObjEligibilityStatus[i].ApplicationIDStatus == "True") {
                    //        $scope.EligibilityStatus = $scope.ObjEligibilityStatus[i].EligibilityStatus;
                    //        $scope.FeeCategoryPartTermMapId = $scope.ObjEligibilityStatus[i].FeeCategoryPartTermMapId;
                    //        $scope.facremarksObj.remarks = $scope.ObjEligibilityStatus[i].AdminRemarkByFaculty
                    //    }
                    //    else if ($scope.ObjEligibilityStatus[i].ApplicationIDStatus == "False") {
                    //        $scope.EligibilityStatus = $scope.ObjEligibilityStatus[i].EligibilityStatus;
                    //        $scope.FeeCategoryPartTermMapId = $scope.ObjEligibilityStatus[i].FeeCategoryPartTermMapId;
                    //        $scope.facremarksObj.remarks = $scope.ObjEligibilityStatus[i].AdminRemarkByFaculty
                    //    }
                    //}

                    $scope.AdminRemarkByFaculty = $scope.facremarksObj.remarks;
                    $scope.getInstituteListByIncPartTermId();
                    $scope.getMstPreferenceGroup();
                    //$scope.getRequiredDocListByProgramme();

                     if ($scope.ApplicantData.objAdmApplicationinfo.PreferenceGroupId != null && $scope.ApplicantData.objAdmApplicationinfo.PreferenceGroupId != "" && $scope.ApplicantData.objAdmApplicationinfo.PreferenceGroupId != undefined) {
                         $scope.PostGetDestinationProgPTID($scope.ApplicantData.objAdmApplicationinfo.PreferenceGroupId);
                    }
                    if ($scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId != null && $scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId != "" && $scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId != undefined) {
                        $scope.getFeeCategoryByDestPTID($scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId);
                    }
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Function for Get Fee Category by Destination PTID
    $scope.getFeeCategoryByDestPTID = function () {

        if ($scope.DestProgID == "" || $scope.DestProgID == null || $scope.DestProgID == undefined) {
            $scope.DestProgID = $scope.InstPartTermId;
        }

        if ($scope.checkDestIDFlag == false) {
            if ($scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId != null) {
                $scope.DestProgID = $scope.ApplicantData.objAdmApplicationinfo.DestinationIncProgInstPartTermId;
            }
        }

        $scope.checkFeeFlag = false;
        $http({
            method: 'POST',
            url: 'api/VerifyApplicantProfileForm/PostGetFeeCategoryByDestPTID',
            data: {
                DestPartTermId: $scope.DestProgID,
                AppId: $localStorage.VerificationAppId,
                Gender: $scope.ObjPersonal.Gender
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    $scope.FeeCategoryList = response.obj;
                    for (let i = 0; i < $scope.FeeCategoryList.length; i++) {

                        if ($scope.ApplicantData.objAdmApplicationinfo.FeeCategoryPartTermMapId == $scope.FeeCategoryList[i].FeeCategoryPartTermMapId) {
                            $scope.FeeCategoryPartTermMapId = $scope.ApplicantData.objAdmApplicationinfo.FeeCategoryPartTermMapId;
                        }
                    }
                    $scope.checkFeeFlag = true;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.selectedInstitute = function (id) {
        $scope.InstituteId = id;
    };

    //$scope.selectedPrefGroup = function (id) {
    //    $scope.PreferenceGroupId = id;
    //    $scope.PostGetDestinationProgPTID();
    //};

    $scope.selectedDestProgID = function (id) {
        $scope.DestProgID = id;
        $scope.checkDestIDFlag = true;
        $scope.getFeeCategoryByDestPTID();
    };

    $scope.selectedFeeCategory = function (id) {
        $scope.FeeCategoryPartTermMapId = id;
    };

    $scope.CheckallVerifyTrue = function () {

        if($scope.checkReAdmissionFlag == true)
        {
            $scope.checkDocFlag = true;
        }
        else
        {
            $scope.checkDocFlag = false;
            var checkEduStatus = true;
            var checkAddonStatus = true;
            var checkSubmittedDocStatus = true;
            var checkAdditionalDocStatus = true;

            for (var i = 0; i < $scope.ObjEducation.length; i++) {
                if ($scope.ObjEducation[i].IsVerified == 'False') {
                    $scope.checkEduStatus = $scope.ObjEducation[i].IsVerified;
                    checkEduStatus = false;
                    break;
                }
            }

            for (var i = 0; i < $scope.ObjAddOn.length; i++) {
                if ($scope.ObjAddOn[i].Id != 0 && $scope.ObjAddOn[i].IsVerified == 'False') {
                    $scope.checkAddonStatus = $scope.ObjAddOn[i].IsVerified;
                    checkAddonStatus = false;
                    break;
                }
            }
            for (var i = 0; i < $scope.ObjSubmittedDoc.length; i++) {
                if (($scope.ObjSubmittedDoc[i].Id != 0 && $scope.ObjSubmittedDoc[i].IsVerified == 'False')
                    || (($scope.ObjSubmittedDoc[i].Id == 0 && $scope.ObjSubmittedDoc[i].IsCompulsoryDocument == "True")
                        && ($scope.ObjSubmittedDoc[i].IsVerified == null))) {
                    $scope.checkSubmittedDocStatus = $scope.ObjSubmittedDoc[i].IsVerified;
                    checkSubmittedDocStatus = false;
                    break;
                }
            }
            for (var i = 0; i < $scope.ObjAdditional.length; i++) {
                if ($scope.ObjAdditional[i].IsVerified == 'False') {
                    $scope.checkAdditionalDocStatus = $scope.ObjAdditional[i].IsVerified;
                    checkAdditionalDocStatus = false;
                    break;
                }
            }
            if ($scope.InstPartTermId != 178 && $scope.InstPartTermId != 188 && $scope.InstPartTermId != 749 && $scope.InstPartTermId != 759) {
                if (($scope.ObjPersonal.IsDOBDocVerified == 'False') ||
                    ($scope.ObjPersonal.IsPhotoIdDocVerified == 'False') ||
                    (($scope.ObjPersonal.IsAadharDocVerified == 'False') && ($scope.ObjPersonal.AadharDoc != 'Not Applicable' && $scope.ObjPersonal.AadharDoc != null)) ||
                    (($scope.ObjPersonal.IsSocialCategoryDocVerified == 'False')
                        && ($scope.ObjPersonal.SocialCategoryId != 0 && $scope.ObjPersonal.SocialCategoryId != null && $scope.ObjPersonal.SocialCategoryCode != 'NA')
                        && ($scope.ObjPersonal.SocialCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.SocialCategoryDoc != null)) ||
                    (($scope.ObjPersonal.IsReservationCategoryDocVerified == 'False')
                        && ($scope.ObjPersonal.ReservationCategoryCode != 'GEN')
                        && ($scope.ObjPersonal.ReservationCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.ReservationCategoryDoc != null)) ||
                    (($scope.ObjPersonal.IsNCLCertiVerified == 'False')
                        && ($scope.ObjPersonal.ReservationCategoryCode == 'SEBC')
                        && ($scope.ObjPersonal.NCLCerti != 'Not Applicable' && $scope.ObjPersonal.NCLCerti != null)) ||
                    (($scope.ObjPersonal.IsEWSDocVerified == 'False')
                        && ($scope.ObjPersonal.IsEWS == 'True')
                        && ($scope.ObjPersonal.EWSDoc != 'Not Applicable' && $scope.ObjPersonal.EWSDoc != null)) ||
                    (($scope.ObjPersonal.IsEWS_IADocVerified == 'False')
                        && ($scope.ObjPersonal.IsEWS == 'True')
                        && ($scope.ObjPersonal.EWS_IADoc != 'Not Applicable' && $scope.ObjPersonal.EWS_IADoc != null)) ||
                    (($scope.ObjPersonal.IsPCDocVerified == 'False') && ($scope.ObjPersonal.IsPhysicallyChallenged == 'True' && ($scope.ObjPersonal.PCDoc != 'Not Applicable' && $scope.ObjPersonal.PCDoc != null))) ||
                    ($scope.ObjPersonal.IsApplicantPhotoVerified == 'False') ||
                    ($scope.ObjPersonal.IsApplicantSignatureVerified == 'False') ||
                    (checkEduStatus == false) ||
                    (checkAddonStatus == false) ||
                    (checkSubmittedDocStatus == false) ||
                    (checkAdditionalDocStatus == false)) {

                    $scope.checkDocFlag = true;
                }
                else {
                    $scope.checkDocFlag = false;
                }
            }
            else if ((checkEduStatus == false) ||
                (checkAddonStatus == false) ||
                (checkSubmittedDocStatus == false) ||
                (checkAdditionalDocStatus == false)) {
                $scope.checkDocFlag = true;
            }
            else {
                $scope.checkDocFlag = false;
            }
        }
    };

    //Function for Update AdmApplicationAdminRemarksByFaculty
    $scope.UpdateAdmApplicationAdminRemarksByFaculty = function () {

        //$scope.verifymsg = ""; 
        if ($scope.DestProgID == null || $scope.DestProgID == undefined) {
            $scope.DestProgID = $scope.InstPartTermId;
        }
       
        $scope.getEmailByKeyName('Fac_Verify_Stu_Email').then(function (response) {
        $scope.EmailKeyName = response.data.obj[0].Value;
            

            var checkEduStatus = true;
            var checkAddonStatus = true;
            var checkSubmittedDocStatus = true;
            var checkAdditionalDocStatus = true;

            for (var i = 0; i < $scope.ObjEducation.length; i++) {
                if ($scope.ObjEducation[i].IsVerified == "False" || $scope.ObjEducation[i].IsVerified == null) {
                    $scope.checkEduStatus = $scope.ObjEducation[i].IsVerified;
                    checkEduStatus = false;
                    break;
                }
            }

            for (var i = 0; i < $scope.ObjAddOn.length; i++) {
                if ($scope.ObjAddOn[i].Id != 0 && ($scope.ObjAddOn[i].IsVerified == "False" || $scope.ObjAddOn[i].IsVerified == null)) {
                    $scope.checkAddonStatus = $scope.ObjAddOn[i].IsVerified;
                    checkAddonStatus = false;
                    break;
                }
            }
            for (var i = 0; i < $scope.ObjSubmittedDoc.length; i++) {
                if ((($scope.ObjSubmittedDoc[i].Id != 0) && ($scope.ObjSubmittedDoc[i].IsVerified == "False" || $scope.ObjSubmittedDoc[i].IsVerified == null))
                       || (($scope.ObjSubmittedDoc[i].Id == 0 && $scope.ObjSubmittedDoc[i].IsCompulsoryDocument == "True")
                        && ($scope.ObjSubmittedDoc[i].IsVerified == null))) {
                    $scope.checkSubmittedDocStatus = $scope.ObjSubmittedDoc[i].IsVerified;
                    checkSubmittedDocStatus = false;
                    break;
                }
            }
            for (var i = 0; i < $scope.ObjAdditional.length; i++) {
                if ($scope.ObjAdditional[i].IsVerified == "False" || $scope.ObjAdditional[i].IsVerified == null) {
                    $scope.checkAdditionalDocStatus = $scope.ObjAdditional[i].IsVerified;
                    checkAdditionalDocStatus = false;
                    break;
                }
            }
            

        var facultyeligibilitystatus = document.getElementById("facultyeligibilitystatus");
        var feename = document.getElementById("feename");
        var institute = document.getElementById("institute");
        var GroupRef = document.getElementById("GroupRef");
        //var DestProgID = document.getElementById("DestProgID");

    
        if (facultyeligibilitystatus.value == "") {
            alert("Please select Eligibility Satus...!");
        }
        else if ((facultyeligibilitystatus.value == 'Provisionally_Approved') &&
            (institute.value == null || institute.value == "" || institute.value === undefined)) {
            alert("Please Select Institute...!");
        }
       
        else if ((facultyeligibilitystatus.value == 'Provisionally_Approved') &&
            (feename.value == null || feename.value == "" || feename.value === undefined)) {
            alert("Please Select Fee...!");
        }
        else if ((facultyeligibilitystatus.value == 'Provisionally_Approved') &&
            ($scope.GroupPrefList.length > 0) &&
            (GroupRef.value == null || GroupRef.value == "" || GroupRef.value === undefined)) {
            alert("Please Select Group...!");
        }
        //else if ((facultyeligibilitystatus.value == 'Provisionally_Approved') &&
        //    ($scope.DestProgList.length > 0) &&
        //    (DestProgID.value == null || DestProgID.value == "" || DestProgID.value === undefined)) {
        //    alert("Please Select Destination Programme...!");
        //}
        else if ((facultyeligibilitystatus.value == 'Not_Approved' || facultyeligibilitystatus.value == 'Pending') &&
            ($scope.facremarksObj.remarks == null || $scope.facremarksObj.remarks == "" || $scope.facremarksObj.remarks === undefined)) {
            alert("Please Add Remarks...!");
        }

        else if (($scope.EligibilityStatus == 'Provisionally_Approved') &&
            (checkEduStatus == false)) {
            alert("Educational Documents Verification Pending.");
        }

        else if ((facultyeligibilitystatus.value == 'Provisionally_Approved') &&
            (checkAddonStatus == false)) {
            alert("Add-On Information Verification Pending.");
        }

        else if ((facultyeligibilitystatus.value == 'Provisionally_Approved') &&
            (checkSubmittedDocStatus == false)) {
            alert("Required Documents Verification Pending.");
        }
            
        else if ((facultyeligibilitystatus.value == 'Provisionally_Approved') &&
            (checkAdditionalDocStatus == false)) {
            alert("Additional Documents Verification Pending.");
        }

        else {
            $scope.CheckFacultyRemarks();
            $scope.CheckPendingDocList();
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/PostApplicantVerification/UpdateAdmApplicationAdminRemarksByFaculty',
                data: {
                    Id: $localStorage.VerificationAppId,
                    ApplicantRegId : $scope.ApplicantRegId,
                    EligibilityStatus: facultyeligibilitystatus.value,
                    AdminRemarkByFaculty: $scope.facremarksObj.remarks,
                    FeeCategoryId: $scope.FeeCategoryId,
                    FeeCategoryPartTermMapId: $scope.FeeCategoryPartTermMapId,
                    ProgrammeInstancePartTermId: $scope.InstPartTermId,
                    //InstituteId: institute.value.split(":")[1] || 0,
                    InstituteId: $scope.InstituteId,
                    PreferenceGroupId: $scope.PreferenceGroupId,
                    DestProgID: $scope.DestProgID,
                    //IsDualAdmission: $scope.IsDualAdmission,
                    FirstName: $scope.ObjPersonal.FirstName,
                    MiddleName: $scope.ObjPersonal.MiddleName,
                    LastName: $scope.ObjPersonal.LastName,
                    MobileNo: $scope.ObjPersonal.MobileNo,
                    EmailId: $scope.ObjPersonal.EmailId,
                    InstPartTerm: $scope.InstPartTerm,
                    ProgrammeName: $localStorage.PostVerify.ProgrammeName,
                    BranchName: $localStorage.PostVerify.BranchName,
                    AcademicYearCode: $localStorage.PostVerify.AcademicYearCode,
                    EmailKeyName: $scope.EmailKeyName,
                    PendingDocList: $scope.PendingDocListFlag
                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $scope.offSpinner();
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $scope.offSpinner();
                        alert(response.obj);
                        $scope.EligibilityStatus = "";
                        //$scope.remarks = "";
                        $scope.FeeCategoryPartTermMapId = 0;
                        //$state.go('PostConfiguration');

                        $localStorage.BacktoPostPage.SubmitFlag = true;
                        $localStorage.BacktoPostPage.FacultyStatus = facultyeligibilitystatus.value;
                        $localStorage.BacktoPostPage.FacultyRemarks = $scope.facremarksObj.remarks;
                        $localStorage.BacktoPostPage.Index1 = $localStorage.VerificationData.map(function (item) { return item.Id; }).indexOf($localStorage.VerificationAppId);
                        $scope.backToPostConfigList();
                    }
                   
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            }
        });
    };

    ////Function for Update AdmApplicationDualAdmissionRequest
    //$scope.UpdateDualAdmissionTableData = function () {

    //    if (($scope.ObjAdmAppInfo.IsDualAdmission == true) &&
    //        ($scope.remarksObj.dualremarks == null || $scope.remarksObj.dualremarks == "" || $scope.remarksObj.dualremarks === undefined)) {
    //        alert("Please Add Remarks for Dual Admission Request...!");
    //    }
    //    else {

    //        $http({
    //            method: 'POST',
    //            url: 'api/PostApplicantVerification/UpdateDualAdmissionRequestByFaculty',
    //            data: {
    //                Id: $localStorage.VerificationAppId,
    //                ApplicantRegId: $scope.ApplicantRegId,
    //                ProgrammeInstancePartTermId: $scope.InstPartTermId,
    //                IsDualAdmission: $scope.ObjAdmAppInfo.IsDualAdmission,
    //                RemarkByFaculty: $scope.remarksObj.dualremarks
    //            },
    //            headers: { "Content-Type": 'application/json' }
    //        })
    //            .success(function (response) {
    //                $rootScope.showLoading = false;

    //                if (response.response_code != "200") {
    //                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //                }
    //                else {
    //                    alert(response.obj);
    //                   //$state.go('PostConfiguration');
    //                    $scope.backToPostConfigList();
    //                }
    //            })
    //            .error(function (res) {
    //                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //            });
    //    }
    //};

    ////Function for Update AdmApplicationDualAdmissionRequest
    //$scope.UpdateWithNoDualProcess = function () {
    //    if ($scope.ObjAdmAppInfo.IsDualAdmission == true) {
    //        $scope.UpdateDualAdmissionTableData();
    //    }
    //    else {
    //        $scope.UpdateAdmApplicationAdminRemarksByFaculty();
    //    }
       
    //};

    //Function for Update AdmApplicationSubmitbutton

    $scope.UpdateDualAdmissionRequest = function () {

        //$scope.submitmsg = "";
        $scope.submitmsgEligible = "";
        $scope.VerificationSuccess = "";
       
        var checkEduStatus = true;
        var checkAddonStatus = true;
        var checkSubmittedDocStatus = true;
        var checkAdditionalDocStatus = true;
    
        for (var i = 0; i < $scope.ObjEducation.length; i++) {
            if ($scope.ObjEducation[i].IsVerified == null) {
                $scope.checkEduStatus = $scope.ObjEducation[i].IsVerified;
                checkEduStatus = false;
                break;
            }
        }

        for (var i = 0; i < $scope.ObjAddOn.length; i++) {
            if ($scope.ObjAddOn[i].Id != 0 && $scope.ObjAddOn[i].IsVerified == null) {
                $scope.checkAddonStatus = $scope.ObjAddOn[i].IsVerified;
                checkAddonStatus = false;
                break;
            }
        }
        
        for (var i = 0; i < $scope.ObjSubmittedDoc.length; i++) {
            if (($scope.ObjSubmittedDoc[i].Id != 0 && $scope.ObjSubmittedDoc[i].IsVerified == null)) {
                //|| (($scope.ObjSubmittedDoc[i].Id == 0 && $scope.ObjSubmittedDoc[i].IsCompulsoryDocument == "True")
                //   && $scope.ObjSubmittedDoc[i].IsVerified == null)) {
                $scope.checkSubmittedDocStatus = $scope.ObjSubmittedDoc[i].IsVerified;
                checkSubmittedDocStatus = false;
                break;
            }
        }

        for (var i = 0; i < $scope.ObjAdditional.length; i++) {
            if ($scope.ObjAdditional[i].IsVerified == null) {
                $scope.checkAdditionalDocStatus = $scope.ObjAdditional[i].IsVerified;
                checkAdditionalDocStatus = false;
                break;
            }
        }

  
        if ($scope.InstPartTermId != 178 && $scope.InstPartTermId != 188 && $scope.InstPartTermId != 749 && $scope.InstPartTermId != 759)
        {
            if ($scope.ObjPersonal.IsDOBDocVerified == null) {
                alert("Birth Certificate Document Verification Pending.");
            }
            else if ($scope.ObjPersonal.IsPhotoIdDocVerified == null) {
                alert("PhotoID Document Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsAadharDocVerified == null) && ($scope.ObjPersonal.AadharDoc != 'Not Applicable' && $scope.ObjPersonal.AadharDoc != null)) {
                alert("Aadhar Card Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsSocialCategoryDocVerified == null) 
                && ($scope.ObjPersonal.SocialCategoryId != 0 && $scope.ObjPersonal.SocialCategoryId != null && $scope.ObjPersonal.SocialCategoryCode != 'NA')
                && ($scope.ObjPersonal.SocialCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.SocialCategoryDoc != null)) {
                alert("Social Category Document Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsReservationCategoryDocVerified == null)
                && ($scope.ObjPersonal.ReservationCategoryCode != 'GEN')
                && ($scope.ObjPersonal.ReservationCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.ReservationCategoryDoc != null)) {
                alert("Reservation Category Document Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsNCLCertiVerified == null)
                && ($scope.ObjPersonal.ReservationCategoryCode == 'SEBC')
                && ($scope.ObjPersonal.NCLCerti != 'Not Applicable' && $scope.ObjPersonal.NCLCerti != null)) {
                alert("Non Creamy Layer Document Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsEWSDocVerified == null)
                && ($scope.ObjPersonal.IsEWS == 'True')
                && ($scope.ObjPersonal.EWSDoc != 'Not Applicable' && $scope.ObjPersonal.EWSDoc != null)) {
                alert("EWS Document Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsEWS_IADocVerified == null)
                && ($scope.ObjPersonal.IsEWS == 'True')
                && ($scope.ObjPersonal.EWS_IADoc != 'Not Applicable' && $scope.ObjPersonal.EWS_IADoc != null)) {
                alert("Income & Assets Document Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsPCDocVerified == null) && ($scope.ObjPersonal.IsPhysicallyChallenged == 'True' && ($scope.ObjPersonal.PCDoc != 'Not Applicable' && $scope.ObjPersonal.PCDoc != null))) {
                alert("Disability Document Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsApplicantPhotoVerified == null)) {
                alert("Applicant Photo Verification Pending.");
            }
            else if (($scope.ObjPersonal.IsApplicantSignatureVerified == null)) {
                alert("Applicant Signature Verification Pending.");
            }

            else if (checkEduStatus == false) {
                alert("Educational Documents Verification Pending.");
            }

            else if (checkAddonStatus == false) {
                alert("Add-On Information Verification Pending.");
            }

            else if (checkSubmittedDocStatus == false) {
                alert("Required Documents Verification Pending.");
            }

            else if (checkAdditionalDocStatus == false) {
                alert("Additional Documents Verification Pending.");
            }
            else {

                $http({
                    method: 'POST',
                    url: 'api/PostApplicantVerification/UpdateFacultyMainSubmit',
                    data: {
                        Id: $localStorage.VerificationAppId,
                        ApplicantRegId: $scope.ApplicantRegId,
                        ProgrammeInstancePartTermId: $scope.InstPartTermId,
                        VerificationStatus: $scope.ObjAdmAppInfo.VerificationStatus,
                        AcademicYearId: $localStorage.AcademicYearId
                    },
                    headers: { "Content-Type": 'application/json' }
                })
                    .success(function (response) {
                        $rootScope.showLoading = false;

                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                            alert(response.obj);
                        }
                        else {

                            //if (response.obj.includes('Student Application is already Approved for the')) {
                            //    $scope.submitmsg = response.obj;
                            //    $scope.submitmsgflag = true;
                            //    $('#btnFinalVerify').hide();
                            //}
                            //else if (response.obj.includes('Please Approved Student Application.')) {
                            //    $scope.submitmsgEligible = response.obj;
                            //    $scope.submitmsgEligibleflag = true;
                            //    $('#btnFinalVerify').hide();
                            //} else {

                            //alert(response.obj);
                            ////$state.go('PostConfiguration');
                            //$scope.backToPostConfigList();

                            if (response.obj.includes('Student has been already admitted last year.')) {
                                alert(response.obj);
                            }
                            if (response.obj.includes('Student has been admitted in ')) {
                               
                                $scope.ViewAdmittedCourse = response.obj;
                                $scope.ViewAdmittedCourseFlag = true;
                                $scope.submitmsgEligibleflag = true;
                                $scope.checkReAdmissionFlag = true;
                                $('#btnFinalVerify').hide();
                            }
                            if (response.obj.includes('Verification Status for this Applicant has been Not Approved in Pre-Verification Process.')) {
                                alert(response.obj);
                            }

                            if (response.obj.includes('Student Application Submited.')) {
                                $scope.submitmsgEligible = response.obj;
                                $scope.submitmsgEligibleflag = true;
                                $('#btnFinalVerify').hide();
                            }

                            if (response.obj.includes('Application has been already approved.')) {
                                $scope.VerificationSuccess = response.obj;
                                $scope.VerificationSuccessflag = true;
                                $scope.submitmsgEligibleflag = true;
                                $('#btnFinalVerify').hide();
                            }
                        }
                    }
                    )
                    .error(function (res) {
                        $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                    });
            }

        }

        else if (checkEduStatus == false) {
            alert("Educational Documents Verification Pending.");
        }

        else if (checkAddonStatus == false) {
            alert("Add-On Information Verification Pending.");
        }

        else if (checkSubmittedDocStatus == false) {
            alert("Required Documents Verification Pending.");
        }

        else if (checkAdditionalDocStatus == false) {
            alert("Additional Documents Verification Pending.");
        }

        else {

            $http({
                method: 'POST',
                url: 'api/PostApplicantVerification/UpdateFacultyMainSubmit',
                data: {
                    Id: $localStorage.VerificationAppId,
                    ApplicantRegId: $scope.ApplicantRegId,
                    ProgrammeInstancePartTermId: $scope.InstPartTermId,
                    VerificationStatus: $scope.ObjAdmAppInfo.VerificationStatus,
                    AcademicYearId: $localStorage.AcademicYearId
                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        
                        //if (response.obj.includes('Student Application is already Approved for the')) {
                        //    $scope.submitmsg = response.obj;
                        //    $scope.submitmsgflag = true;
                        //    $('#btnFinalVerify').hide();
                        //}
                        //else if (response.obj.includes('Please Approved Student Application.')) {
                        //    $scope.submitmsgEligible = response.obj;
                        //    $scope.submitmsgEligibleflag = true;
                        //    $('#btnFinalVerify').hide();
                        //} else {
                       
                            //alert(response.obj);
                            ////$state.go('PostConfiguration');
                            //$scope.backToPostConfigList();

                        if (response.obj.includes('Student has been already admitted last year.')) {
                            alert(response.obj);
                        }
                        if (response.obj.includes('Student has been admitted in ')) {
                          
                            $scope.ViewAdmittedCourse = response.obj;
                            $scope.ViewAdmittedCourseFlag = true;
                            $scope.submitmsgEligibleflag = true;
                            $scope.checkReAdmissionFlag = true;
                            $('#btnFinalVerify').hide();
                        }
                        if (response.obj.includes('Verification Status for this Applicant has been Not Approved in Pre-Verification Process.')) {
                            alert(response.obj);
                        }

                        if (response.obj.includes('Student Application Submited.')) {
                            $scope.submitmsgEligible = response.obj;
                            $scope.submitmsgEligibleflag = true;
                            $('#btnFinalVerify').hide();
                            }
                        if (response.obj.includes('Application has been already approved.')) {
                            $scope.VerificationSuccess = response.obj;
                            $scope.VerificationSuccessflag = true;
                            $scope.submitmsgEligibleflag = true;
                            $('#btnFinalVerify').hide();
                        }

                        }
                    }
                )
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }     
    };

    //Function for Update AdmApplicationCategoryRemarks
    $scope.UpdateAdmApplicationCategoryRemarks = function () {

        $scope.ObjAdmAppInfo.Id = $localStorage.VerificationAppId;

        if ($scope.ObjAdmAppInfo.MainCategoryRemarks == null || $scope.ObjAdmAppInfo.MainCategoryRemarks == "" || $scope.ObjAdmAppInfo.MainCategoryRemarks === undefined) {
            alert("Please Add Category Remarks...!");
        }
        
        else {

            $http({
                method: 'POST',
                url: 'api/PostApplicantVerification/UpdateAdmApplicationCategoryRemarks',
                data: $scope.ObjAdmAppInfo,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        btnPersonal.disabled = true;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    ////Function for Update AdmApplication CategoryVerification
    //$scope.UpdateAdmApplicationCategoryVerification = function (ApplicantData) {
    //    $http({
    //        method: 'POST',
    //        url: 'api/PostApplicantVerification/UpdateAdmApplicationCategoryVerification',
    //        data: {
    //            Id: $localStorage.VerificationAppId,
    //            CategoryVerification: ApplicantData.objAdmApplicationinfo.CategoryVerification
    //        },
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                //if (ObjAdmAppCategory.CategoryVerification == "True") {
    //                //    alert("Category Un-Verified Successfully.");
    //                //} else {
    //                //    alert("Category Verified Successfully.");
    //                //}
    //                alert(response.obj);
    //                $scope.ObjAdmAppInfo.CategoryVerification = ApplicantData.objAdmApplicationinfo.CategoryVerification;
    //                $scope.docName = "";
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });

    //};

    ////Function for Update AdmApplication DisabilityVerification
    //$scope.UpdateAdmApplicationDisabilityVerification = function (ObjAdmAppDisability) {
    //    var DisabilityVerification;
    //    if (ObjAdmAppDisability.DisabilityVerification == "True") {
    //        DisabilityVerification = "False";
    //    } else {
    //        DisabilityVerification = "True";
    //    }
    //    $http({
    //        method: 'POST',
    //        url: 'api/PostApplicantVerification/UpdateAdmApplicationDisabilityVerification',
    //        data: {
    //            Id: $localStorage.VerificationAppId,
    //            DisabilityVerification: DisabilityVerification
    //        },
    //        headers: { "Content-Type": 'application/json' }
    //    })
    //        .success(function (response) {
    //            $rootScope.showLoading = false;

    //            if (response.response_code != "200") {
    //                $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
    //            }
    //            else {
    //                alert(response.obj);
    //                $scope.ObjAdmAppInfo.DisabilityVerification = DisabilityVerification;
    //            }
    //        })
    //        .error(function (res) {
    //            $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
    //        });

    //};

    //Function for Update AdmApplicationEduStatus By Id

    $scope.UpdateAdmApplicationEduStatus = function (ObjEduId, index) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateAdmApplicationEduStatusByID',
            data: {
                RegId: $localStorage.VerificationAppRegId,
                ObjEduId: ObjEduId.Id,
                IsVerified: ObjEduId.IsVerified

            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjEducation[index].IsVerified = ObjEduId.IsVerified;
                    $scope.docName = "";
                    $scope.DocTableIndex = -1;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update PostAdmStudentAddOnInfoStatus
    $scope.UpdatePostAdmStudentAddOnInfoStatus = function (AddOnCriteriaId, index) {
   
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdatePostAdmStudentAddOnInfoStatus',
            data: {
                ApplicationId: $localStorage.VerificationAppId,
                AddOnId: AddOnCriteriaId.Id,
                IsVerified: AddOnCriteriaId.IsVerified

            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjAddOn[index].IsVerified = AddOnCriteriaId.IsVerified;
                    $scope.docName = "";
                    $scope.DocTableIndex = -1;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update AdmApplicantSubmittedDocStatus
    $scope.UpdateAdmApplicantSubmittedDocStatus = function (sdocumentId, index) {
  
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateAdmApplicantSubmittedDocStatus',
            data: {
                AdmissionApplicationId: $localStorage.VerificationAppId,
                SubDocId: sdocumentId.Id,
                IsVerified: sdocumentId.IsVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjSubmittedDoc[index].IsVerified = sdocumentId.IsVerified;
                    $scope.docName = "";
                    $scope.DocTableIndex = -1;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update AdmApplicantAdditionalDocStatus
    $scope.UpdateAdmApplicantAdditionalDocStatus = function (additionalId, index) {
 
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateAdmApplicantAdditionalDocStatus',
            data: {
                AdmApplicationId: $localStorage.VerificationAppId,
                AdditionalDocId: additionalId.Id,
                IsVerified: additionalId.IsVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjAdditional[index].IsVerified = additionalId.IsVerified;
                    $scope.docName = "";
                    $scope.DocTableIndex = -1;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for go back to list of Post Configuration Page
    $scope.backToPostConfigList = function () {
        $state.go('PostConfiguration');
        $localStorage.BacktoPostPage.Flag = true;
        $localStorage.BacktoPostPage.AcademicYearId = $scope.AcademicYearId;
        $localStorage.BacktoPostPage.ProgPTID = $scope.InstPartTermId;
        $localStorage.BacktoPostPage.EligibilityStatus = $scope.localEligibilityStatus;
    };

    //Function for check old and new FacultyRemarks
    $scope.CheckFacultyRemarks = function () {

        if ($scope.facremarksObj.remarks != null && $scope.facremarksObj.remarks != "" && $scope.facremarksObj.remarks != undefined) {
            var A = "";
            var B = "";
            var C = "";
            var diff = "";

            A = $scope.AdminRemarkByFaculty;

            B = $scope.facremarksObj.remarks;

            diff = (diffMe, diffBy) => diffMe.split(diffBy).join('')

            C = diff(B, A)
            $scope.facremarksObj.remarks = C;
        }
        
    };

    //Function for Update DOB Doc Verification from AdmApplicationRegistration
    $scope.UpdateDOBDocVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateDOBDocVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsDOBDocVerified: ApplicantData.objApplicant.IsDOBDocVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsDOBDocVerified = ApplicantData.objApplicant.IsDOBDocVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update PhotoId Doc Verification from AdmApplicationRegistration
    $scope.UpdatePhotoIDVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdatePhotoIDVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsPhotoIdDocVerified: ApplicantData.objApplicant.IsPhotoIdDocVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsPhotoIdDocVerified = ApplicantData.objApplicant.IsPhotoIdDocVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update Aadhar Doc Verification from AdmApplicationRegistration
    $scope.UpdateAadharDocVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateAadharDocVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsAadharDocVerified: ApplicantData.objApplicant.IsAadharDocVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsAadharDocVerified = ApplicantData.objApplicant.IsAadharDocVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update Social Category Doc Verification from AdmApplicationRegistration
    $scope.UpdateSocialCategoryDocVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateSocialCategoryDocVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsSocialCategoryDocVerified: ApplicantData.objApplicant.IsSocialCategoryDocVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsSocialCategoryDocVerified = ApplicantData.objApplicant.IsSocialCategoryDocVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update Reservation Category Doc Verification from AdmApplicationRegistration
    $scope.UpdateReservationCategoryDocVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateReservationCategoryDocVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsReservationCategoryDocVerified: ApplicantData.objApplicant.IsReservationCategoryDocVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsReservationCategoryDocVerified = ApplicantData.objApplicant.IsReservationCategoryDocVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update EWS Doc Verification from AdmApplicationRegistration
    $scope.UpdateEWSVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateEWSVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsEWSDocVerified: ApplicantData.objApplicant.IsEWSDocVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsEWSDocVerified = ApplicantData.objApplicant.IsEWSDocVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update PC Doc Verification from AdmApplicationRegistration
    $scope.UpdatePCDocVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdatePCDocVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsPCDocVerified: ApplicantData.objApplicant.IsPCDocVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsPCDocVerified = ApplicantData.objApplicant.IsPCDocVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update Applicant Photo Verification from AdmApplicationRegistration
    $scope.UpdateApplicantPhotoVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateApplicantPhotoVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsApplicantPhotoVerified: ApplicantData.objApplicant.IsApplicantPhotoVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsApplicantPhotoVerified = ApplicantData.objApplicant.IsApplicantPhotoVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update Applicant Signature Verification from AdmApplicationRegistration
    $scope.UpdateApplicantSignatureVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateApplicantSignatureVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsApplicantSignatureVerified: ApplicantData.objApplicant.IsApplicantSignatureVerified
            },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    //alert(response.obj);
                    $scope.ObjPersonal.IsApplicantSignatureVerified = ApplicantData.objApplicant.IsApplicantSignatureVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Check-Uncheck Documents Verification
    $scope.CheckUncheckDocVerification = function (docName, index = -1, docStatusValue, docObject) {

        $scope.docName = docName;
        if (index > -1) {
            $scope.DocTableIndex = index;
        }
        if ($scope.docName == "DOB_doc") {
            docObject.objApplicant.IsDOBDocVerified = docStatusValue;
            $scope.UpdateDOBDocVerification(docObject);
        }
        if ($scope.docName == "PhotoID_doc") {
            docObject.objApplicant.IsPhotoIdDocVerified = docStatusValue;
            $scope.UpdatePhotoIDVerification(docObject);
        }
        if ($scope.docName == "Aadhar_doc") {
            docObject.objApplicant.IsAadharDocVerified = docStatusValue;
            $scope.UpdateAadharDocVerification(docObject);
        }
        if ($scope.docName == "category_doc") {
            docObject.objApplicant.IsSocialCategoryDocVerified = docStatusValue;
            $scope.UpdateSocialCategoryDocVerification(docObject);
        }
        if ($scope.docName == "Res_category_doc") {
            docObject.objApplicant.IsReservationCategoryDocVerified = docStatusValue;
            $scope.UpdateReservationCategoryDocVerification(docObject);
        }
        if ($scope.docName == "NCLCerti_doc") {
            docObject.objApplicant.IsNCLCertiVerified = docStatusValue;
            $scope.UpdateNCLCertiVerification(docObject);
        }
        if ($scope.docName == "EWS_IA_doc") {
            docObject.objApplicant.IsEWS_IADocVerified = docStatusValue;
            $scope.UpdateEWS_IADocVerification(docObject);
        }
        if ($scope.docName == "EWS_doc") {
            docObject.objApplicant.IsEWSDocVerified = docStatusValue;
            $scope.UpdateEWSVerification(docObject);
        }
        if ($scope.docName == "disability_doc") {
            docObject.objApplicant.IsPCDocVerified = docStatusValue;
            $scope.UpdatePCDocVerification(docObject);
        }
        if ($scope.docName == "Photo_doc") {
            docObject.objApplicant.IsApplicantPhotoVerified = docStatusValue;
            $scope.UpdateApplicantPhotoVerification(docObject);
        }
        if ($scope.docName == "Signature_doc") {
            docObject.objApplicant.IsApplicantSignatureVerified = docStatusValue;
            $scope.UpdateApplicantSignatureVerification(docObject);
        }

        if ($scope.DocTableIndex < $scope.ObjEducation.length && $scope.docName == $scope.ObjEducation[$scope.DocTableIndex].EligibleDegreeName) {
            $scope.ObjEducation[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdateAdmApplicationEduStatus($scope.ObjEducation[$scope.DocTableIndex], $scope.DocTableIndex);
        }
        if ($scope.DocTableIndex < $scope.ObjSubmittedDoc.length && $scope.docName == $scope.ObjSubmittedDoc[$scope.DocTableIndex].NameOfTheDocument) {
            $scope.ObjSubmittedDoc[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdateAdmApplicantSubmittedDocStatus($scope.ObjSubmittedDoc[$scope.DocTableIndex], $scope.DocTableIndex);
        }

        if ($scope.DocTableIndex < $scope.ObjAddOn.length && $scope.docName == $scope.ObjAddOn[$scope.DocTableIndex].TitleName) {
            $scope.ObjAddOn[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdatePostAdmStudentAddOnInfoStatus($scope.ObjAddOn[$scope.DocTableIndex], $scope.DocTableIndex);
        }
        if ($scope.DocTableIndex < $scope.ObjAdditional.length && $scope.docName == $scope.ObjAdditional[$scope.DocTableIndex].DocName) {
            $scope.ObjAdditional[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdateAdmApplicantAdditionalDocStatus($scope.ObjAdditional[$scope.DocTableIndex], $scope.DocTableIndex);
        }
    }

    $scope.getdocName = function (docName, index = -1) {
        $scope.docName = docName;
        if (index > -1) {
            $scope.DocTableIndex = index;
        }
    }

    $scope.updateDocStatus = function (docStatusValue, docObject) {
  
        if ($scope.docName == "DOB_doc") {
            docObject.objApplicant.IsDOBDocVerified = docStatusValue;
            $scope.UpdateDOBDocVerification(docObject);
        }
        if ($scope.docName == "PhotoID_doc") {
            docObject.objApplicant.IsPhotoIdDocVerified = docStatusValue;
            $scope.UpdatePhotoIDVerification(docObject);
        }
        if ($scope.docName == "Aadhar_doc") {
            docObject.objApplicant.IsAadharDocVerified = docStatusValue;
            $scope.UpdateAadharDocVerification(docObject);
        }
        if ($scope.docName == "category_doc") {
            docObject.objApplicant.IsSocialCategoryDocVerified = docStatusValue;
            $scope.UpdateSocialCategoryDocVerification(docObject);
        }
        if ($scope.docName == "Res_category_doc") {
            docObject.objApplicant.IsReservationCategoryDocVerified = docStatusValue;
            $scope.UpdateReservationCategoryDocVerification(docObject);
        }
        if ($scope.docName == "EWS_doc") {
            docObject.objApplicant.IsEWSDocVerified = docStatusValue;
            $scope.UpdateEWSVerification(docObject);
        }
        if ($scope.docName == "disability_doc") {
            docObject.objApplicant.IsPCDocVerified = docStatusValue;
            $scope.UpdatePCDocVerification(docObject);
        }
        if ($scope.docName == "Photo_doc") {
            docObject.objApplicant.IsApplicantPhotoVerified = docStatusValue;
            $scope.UpdateApplicantPhotoVerification(docObject);
        }
        if ($scope.docName == "Signature_doc") {
            docObject.objApplicant.IsApplicantSignatureVerified = docStatusValue;
            $scope.UpdateApplicantSignatureVerification(docObject);
        }
    
        if ($scope.DocTableIndex < $scope.ObjEducation.length && $scope.docName == $scope.ObjEducation[$scope.DocTableIndex].EligibleDegreeName) {
            $scope.ObjEducation[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdateAdmApplicationEduStatus($scope.ObjEducation[$scope.DocTableIndex], $scope.DocTableIndex);
        }
        if ($scope.DocTableIndex < $scope.ObjSubmittedDoc.length && $scope.docName == $scope.ObjSubmittedDoc[$scope.DocTableIndex].NameOfTheDocument) {
            $scope.ObjSubmittedDoc[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdateAdmApplicantSubmittedDocStatus($scope.ObjSubmittedDoc[$scope.DocTableIndex], $scope.DocTableIndex);
        }
   
        if ($scope.DocTableIndex < $scope.ObjAddOn.length &&  $scope.docName == $scope.ObjAddOn[$scope.DocTableIndex].TitleName) {
            $scope.ObjAddOn[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdatePostAdmStudentAddOnInfoStatus($scope.ObjAddOn[$scope.DocTableIndex], $scope.DocTableIndex);
        }
        if ($scope.DocTableIndex < $scope.ObjAdditional.length && $scope.docName == $scope.ObjAdditional[$scope.DocTableIndex].DocName) {
            $scope.ObjAdditional[$scope.DocTableIndex].IsVerified = docStatusValue;
            $scope.UpdateAdmApplicantAdditionalDocStatus($scope.ObjAdditional[$scope.DocTableIndex], $scope.DocTableIndex);
        } 
        
    }

    $scope.CheckPendingDocList = function () {

        var checkEduStatus = true;
        var checkSubmittedDocStatus = true;
        var checkAdditionalDocStatus = true;

        for (var i = 0; i < $scope.ObjEducation.length; i++) {
            if ($scope.ObjEducation[i].IsVerified == 'False') {
                $scope.checkEduStatus = $scope.ObjEducation[i].IsVerified;
                checkEduStatus = false;
                break;
            }
        }

        for (var i = 0; i < $scope.ObjSubmittedDoc.length; i++) {
            if (($scope.ObjSubmittedDoc[i].Id != 0 && $scope.ObjSubmittedDoc[i].IsVerified == 'False')
                || (($scope.ObjSubmittedDoc[i].Id == 0 && $scope.ObjSubmittedDoc[i].IsCompulsoryDocument == "True")
                && ($scope.ObjSubmittedDoc[i].IsVerified == null))) {
                $scope.checkSubmittedDocStatus = $scope.ObjSubmittedDoc[i].IsVerified;
                checkSubmittedDocStatus = false;
                break;
            }
        }
        for (var i = 0; i < $scope.ObjAdditional.length; i++) {
            if ($scope.ObjAdditional[i].IsVerified == 'False') {
                $scope.checkAdditionalDocStatus = $scope.ObjAdditional[i].IsVerified;
                checkAdditionalDocStatus = false;
                break;
            }
        }

            if ($scope.ObjPersonal.IsDOBDocVerified == 'False') {
                $scope.ObjPersonal.IsDOBDocVerified = "-Date of Birth ";
            }
            else {
                $scope.ObjPersonal.IsDOBDocVerified = "";
            }

            if ($scope.ObjPersonal.IsPhotoIdDocVerified == 'False') {
                $scope.ObjPersonal.IsPhotoIdDocVerified = "-PhotoID ";
            }
            else {
                $scope.ObjPersonal.IsPhotoIdDocVerified = "";
            }

            if (($scope.ObjPersonal.IsAadharDocVerified == 'False') && ($scope.ObjPersonal.AadharDoc != 'Not Applicable' && $scope.ObjPersonal.AadharDoc != null)) {
                $scope.ObjPersonal.IsAadharDocVerified = "-Aadhar Card ";
            }
            else {
                $scope.ObjPersonal.IsAadharDocVerified = "";
            }

        if (($scope.ObjPersonal.IsSocialCategoryDocVerified == 'False')
            && ($scope.ObjPersonal.SocialCategoryId != 0 && $scope.ObjPersonal.SocialCategoryId != null && $scope.ObjPersonal.SocialCategoryCode != 'NA')
            && ($scope.ObjPersonal.SocialCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.SocialCategoryDoc != null)) {
                $scope.ObjPersonal.IsSocialCategoryDocVerified = "-Social Category ";
            }
            else {
                $scope.ObjPersonal.IsSocialCategoryDocVerified = "";
            }

        if (($scope.ObjPersonal.IsReservationCategoryDocVerified == 'False')
            && ($scope.ObjPersonal.ReservationCategoryCode != 'GEN')
            && ($scope.ObjPersonal.ReservationCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.ReservationCategoryDoc != null)) {
                $scope.ObjPersonal.IsReservationCategoryDocVerified = "-Reservation Category ";
            }
            else {
                $scope.ObjPersonal.IsReservationCategoryDocVerified = "";
            }

            if (($scope.ObjPersonal.IsEWSDocVerified == 'False')
                && ($scope.ObjPersonal.IsEWS == 'True')
                && ($scope.ObjPersonal.EWSDoc != 'Not Applicable' && $scope.ObjPersonal.EWSDoc != null)) {
                    $scope.ObjPersonal.IsEWSDocVerified = "-EWS ";
                }
            else {
                $scope.ObjPersonal.IsEWSDocVerified = "";
            }


            if (($scope.ObjPersonal.IsNCLCertiVerified == 'False')
                && ($scope.ObjPersonal.ReservationCategoryCode == 'SEBC')
                && ($scope.ObjPersonal.NCLCerti != 'Not Applicable' && $scope.ObjPersonal.NCLCerti != null)) {
                $scope.ObjPersonal.IsNCLCertiVerified = "-NCL Certificate ";
                }
            else {
                $scope.ObjPersonal.IsNCLCertiVerified = "";
            }

           
            if (($scope.ObjPersonal.IsPCDocVerified == 'False') && ($scope.ObjPersonal.IsPhysicallyChallenged == 'True' && ($scope.ObjPersonal.PCDoc != 'Not Applicable' && $scope.ObjPersonal.PCDoc != null))) {
                $scope.ObjPersonal.IsPCDocVerified = "-Disability Document ";
            }
            else {
                $scope.ObjPersonal.IsPCDocVerified = "";
            }

            if ($scope.ObjPersonal.IsApplicantPhotoVerified == 'False') {
                $scope.ObjPersonal.IsApplicantPhotoVerified = "-Photo ";
            }
            else {
                $scope.ObjPersonal.IsApplicantPhotoVerified = "";
            }

            if ($scope.ObjPersonal.IsApplicantSignatureVerified == 'False') {
                $scope.ObjPersonal.IsApplicantSignatureVerified = "-Signature ";
            }
            else {
                $scope.ObjPersonal.IsApplicantSignatureVerified = "";
            }
    

            if (checkEduStatus == false) {
                checkEduStatus = "-Education Document ";
            }
            else {
                checkEduStatus = "";
            }

            if (checkSubmittedDocStatus == false) {
                checkSubmittedDocStatus = "-Required Document ";
            }
            else {
                checkSubmittedDocStatus = "";
            }

            if (checkAdditionalDocStatus == false) {
                checkAdditionalDocStatus = "-Additional Document ";
            }
            else {
                checkAdditionalDocStatus = "";
            }

            $scope.PendingDocListFlag = ($scope.ObjPersonal.IsDOBDocVerified + $scope.ObjPersonal.IsPhotoIdDocVerified + $scope.ObjPersonal.IsAadharDocVerified + $scope.ObjPersonal.IsSocialCategoryDocVerified +
                $scope.ObjPersonal.IsReservationCategoryDocVerified + $scope.ObjPersonal.IsEWSDocVerified + $scope.ObjPersonal.IsNCLCertiVerified + $scope.ObjPersonal.IsPCDocVerified + $scope.ObjPersonal.IsApplicantPhotoVerified + $scope.ObjPersonal.IsApplicantSignatureVerified +
                checkEduStatus + checkSubmittedDocStatus + checkAdditionalDocStatus);
    };


    /*---------------- Start Function AddOn Criteria Details----------------*/

    //Function for go back to Student Add-On Page
    $scope.backToStudentAddOn = function () {
        $state.go('PostAdmProgrammeAddOnCriteria');
    };
    
    //Function for get Add-on Criteria
    $scope.getAddOnCriteria = function () {
        $scope.ProgrammeName = $localStorage.PostVerify.ProgrammeName;
        $scope.BranchName = $localStorage.PostVerify.BranchName;
        $scope.AcademicYearCode = $localStorage.PostVerify.AcademicYearCode;

        $http({
            method: 'Post',
            url: 'api/PostVerificationAddOnCriteria/AdmProgrammeAddOnCriteriaGetbyApplicationId',
            data: {
                ProgrammeInstancePartTermId: $localStorage.InstancePartTermId,
                PostAdmApplicationId: $localStorage.VerificationAppId
            },

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c3List = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //Function for add Add-on Criteria
    $scope.addAddOnCriteriaDetails = function (Id, value, Type) {

        if (value == null || value === undefined || value == ""
        ) {
            //$scope.modifyUserFlag = true;
            //$mdDialog.show(
            //    $mdDialog.alert()
            //        .parent(angular.element(document.querySelector('#AdmApplicantsSubmittedDocumentsform')))
            //        .clickOutsideToClose(true)
            //        .title("Error")
            //        .textContent("Please Enter Marks/CGPA/Marks before Add...")
            //        .ariaLabel('Alert Dialog Demo')
            //        .ok('Okay!')
            //);
            alert("Please Enter Marks/CGPA/Marks before Add...!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/PostVerificationAddOnCriteria/AdmStudentAddOnInformationAddbyApplicationId',
                data: {
                    AdmApplicationId: $localStorage.VerificationAppId,
                    ProgrammeAddOnCriteriaId: Id,
                    AddOnValue: value,
                    TitleType: Type
                },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.getAddOnCriteria();
                            //$state.go('AdmProgrammeAddOnCriteria');

                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    //Function for go back to Student Add-On Page
    $scope.backToFacultyVerificationPage = function () {
        $state.go('PostApplicantVerification');
    };

/*---------------- End Function AddOn Criteria Details----------------*/


});