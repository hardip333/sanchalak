
app.controller('PreApplicantVerificationCtrl', function ($scope, $http, $rootScope, Upload, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Pre Applicant Verification";
    //$scope.PostApplicantConfig = {};
    var verifymsg = "";
    var submitmsg = "";
    var submitmsgEligible = ""; 
    $scope.VerificationStatus = "";
    $scope.EligibilityStatus = "";
    $scope.remarks = "";
    $scope.facremarksObj = { remarks: "" }
   
    $scope.remarksObj = { dualremarks: ""}
    $scope.ApplicantData = {};
    $scope.ProgrammeInstancePartTermId = 0;
    $scope.docName = "";
    $scope.DocTableIndex = -1;
    $scope.PendingDocListFlag = "";
    $scope.checkDestIDFlag = false;
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

    //Genric Config for file size-Dynamic Validation
    $scope.byId = function (data) {
        
        return $http({
            method: 'POST',
            url: 'api/FeeCategoryChange/GenericConfigurationGetById',
            data: { Id: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

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

    $scope.DateRangeforResultStatus = function () {
        
        $scope.currentDate = new Date();
        $scope.currentDate.setHours(0, 0, 0, 0);


        //Start - Use for converting date as dd-mm-yyyy from Universal time format
        $scope.NewcurrentDate = $scope.currentDate;
        let date = new Date($scope.NewcurrentDate);
        let dd = date.getDate();
        let mm = date.getMonth() + 1;
        let yyyy = date.getFullYear();
        $scope.currentDateFinal = (dd + "-" + mm + "-" + yyyy);
        //End - Use for converting date as dd-mm-yyyy from Universal time format

     
        if ($scope.currentDateFinal <= '30-7-2022') {
            $scope.ShowFlagPendingStatus = true;
        }
        else {
            $scope.ShowFlagPendingStatus = false;
        }

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

        $scope.localEligibilityStatus = $localStorage.VerificationStatus;

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

                    
                    $scope.VerificationStatus = $scope.ObjAdmAppInfo.VerificationStatus;
                    $scope.facremarksObj.remarks = $scope.ObjAdmAppInfo.VerificationRemarks;
                  
                    $scope.VerificationRemarks = $scope.facremarksObj.remarks;
                    
                    $scope.AdmApplicantRegiUserName = $scope.ObjEducation[0].AdmApplicantRegiUserName;

                    //For Selected SocialCategoryId Radio
                    if ($scope.ObjPersonal.SocialCategoryId == null || $scope.ObjPersonal.SocialCategoryId == undefined || $scope.ObjPersonal.SocialCategoryId == "") {
                        $scope.CategoryRadioValue = "False";
                        document.getElementById('ifSocialYes').style.display = 'none';
                    }
                    else {
                        $scope.CategoryRadioValue = "True";
                        document.getElementById('ifSocialYes').style.display = '';
                    }

                    //For Selected IsEWS Radio
                    if ($scope.ObjPersonal.IsEWS == 1 || $scope.ObjPersonal.IsEWS == "True") {
                        $scope.ObjPersonal.IsEWS = "True";
                    }
                    else {
                        $scope.ObjPersonal.IsEWS = "False";
                    }
                    
                    //For Selected SocialCategoryId Radio
                    if ($scope.ObjPersonal.ApplicationCategoryId == 3 && $scope.ObjPersonal.IsEWS == "True") {
                        document.getElementById('iYes').style.display = '';
                        document.getElementById('iYes1').style.display = '';
                        document.getElementById('iYes2').style.display = '';
                    }
                    else {
                        document.getElementById('iYes').style.display = 'none';
                        document.getElementById('iYes1').style.display = 'none';
                        document.getElementById('iYes2').style.display = 'none';
                    }
                   
                    
                    //For Check SocialCategoryDoc already uploaded.
                    var scDocSplit = null;
                    var scDoc = $scope.ObjPersonal.SocialCategoryDoc;
                    if (scDoc != undefined && scDoc != "" && scDoc != null) {

                        scDocSplit = scDoc.split("/").pop();
                    }
                    if (scDocSplit != "Not Applicable" && $scope.ObjPersonal.SocialCategoryDoc != null) {

                        $scope.SocialCategoryDocView = $scope.ObjPersonal.SocialCategoryDoc;
                        document.getElementById("SuccessMsgSocialCat").innerHTML = "File already uploaded";
                        $scope.SocCatDocCheck = "File already uploaded";
                        //$scope.ObjPersonal.SocialCategoryDoc = null;
                    }
                    
                    //For Check ReservationCategoryDoc already uploaded.
                    var rcDocSplit = null;
                    var rcDoc = $scope.ObjPersonal.ReservationCategoryDoc;
                    if (rcDoc != undefined && rcDoc != "" && rcDoc != null) {

                        rcDocSplit = rcDoc.split("/").pop();
                    }
                    if (rcDocSplit != "Not Applicable" && $scope.ObjPersonal.ReservationCategoryDoc != null) {

                        $scope.ResCatDocView = $scope.ObjPersonal.ReservationCategoryDoc;
                        document.getElementById("SuccessMsgReservationCat").innerHTML = "File already uploaded";
                        $scope.ResCatDocCheck = "File already uploaded";
                        //$scope.SocialReservationDetailSaved.ReservationCategoryDoc = null;
                    }
                    if (rcDocSplit == "Not Applicable" && $scope.ObjPersonal.IsReservationDocSubmitted != 0) {

                        document.getElementById("SuccessMsgReservationCat").innerHTML = "";
                        $scope.ApplicantReservationRegistration = true;
                        $scope.IsReservationVisible = null;
                        $scope.ObjPersonal.ReservationCategoryDoc = rcDocSplit;
                    }

                    
                    //For Check EWSDoc already uploaded.
                    var ecDocSplit = null;
                    var ecDoc = $scope.ObjPersonal.EWSDoc;
                    if (ecDoc != undefined && ecDoc != "" && ecDoc != null) {

                        ecDocSplit = ecDoc.split("/").pop();
                    }
                    if (ecDocSplit != "Not Applicable" && $scope.ObjPersonal.EWSDoc != null) {

                        $scope.EWSDocView = $scope.ObjPersonal.EWSDoc;
                        document.getElementById("SuccessMsgEWS").innerHTML = "File already uploaded";
                        $scope.EWSDocCheck = "File already uploaded";
                        //$scope.ObjPersonal.EWSDoc = null;
                    }
                    if (ecDocSplit == "Not Applicable" && $scope.ObjPersonal.IsEWSDocSubmitted != 0) {

                        document.getElementById("SuccessMsgEWS").innerHTML = "";
                        $scope.ApplicantEwsRegistration = true;
                        $scope.IsEwsVisible = null;
                    }
                    
                    //For Check NCLCerti already uploaded.
                    var nclDocSplit = null;
                    var nclDoc = $scope.ObjPersonal.NCLCerti;
                    if (nclDoc != undefined && nclDoc != "" && nclDoc != null) {

                        nclDocSplit = nclDoc.split("/").pop();
                    }
                    if (nclDocSplit != "Not Applicable" && $scope.ObjPersonal.NCLCerti != null) {
                        
                        $scope.NCLCertiView = $scope.ObjPersonal.NCLCerti;
                        document.getElementById("SuccessMsgNCLCerti").innerHTML = "File already uploaded";
                        $scope.NCLCertiCheck = "File already uploaded";
                        //$scope.ObjPersonal.NCLCerti = null;

                    }

                    //For Check EWS_IA already uploaded.
                    var ews_IADocSplit = null;
                    var ews_IADoc = $scope.ObjPersonal.EWS_IADoc;
                    if (ews_IADoc != undefined && ews_IADoc != "" && ews_IADoc != null) {

                        ews_IADocSplit = ews_IADoc.split("/").pop();
                    }
                    if (ews_IADocSplit != "Not Applicable" && $scope.ObjPersonal.EWS_IADoc != null) {

                        $scope.EWS_IAView = $scope.ObjPersonal.EWS_IADoc;
                        document.getElementById("SuccessMsgEWS_IADoc").innerHTML = "File already uploaded";
                        $scope.EWS_IADocCheck = "File already uploaded";
                        //$scope.ObjPersonal.NCLCerti = null;

                    }
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.CheckallVerifyTrue = function () {
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

    };

    //Function for Update AdmApplicationAdminRemarksByFaculty
    $scope.UpdateAdmApplicationAdminRemarksByFaculty = function () {
       
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
            

            var VerificationStatus = document.getElementById("VerificationStatus");
    
            if (VerificationStatus.value == "") {
            alert("Please select Eligibility Satus...!");
        }
      
            else if ((VerificationStatus.value == 'Not_Approved' || VerificationStatus.value == 'Pending') &&
            ($scope.facremarksObj.remarks == null || $scope.facremarksObj.remarks == "" || $scope.facremarksObj.remarks === undefined)) {
            alert("Please Add Remarks...!");
        }

            else if (($scope.VerificationStatus == 'Verified') &&
            (checkEduStatus == false)) {
            alert("Educational Documents Verification Pending.");
        }

            else if ((VerificationStatus.value == 'Verified') &&
            (checkAddonStatus == false)) {
            alert("Add-On Information Verification Pending.");
        }

            else if ((VerificationStatus.value == 'Verified') &&
            (checkSubmittedDocStatus == false)) {
            alert("Required Documents Verification Pending.");
        }
            
            else if ((VerificationStatus.value == 'Verified') &&
            (checkAdditionalDocStatus == false)) {
            alert("Additional Documents Verification Pending.");
        }

        else {
            $scope.CheckFacultyRemarks();
            $scope.CheckPendingDocList();
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/PostApplicantVerification/UpdateDeatilsforPreConfiguration',
                data: {
                    Id: $localStorage.VerificationAppId,
                    ApplicantRegId : $scope.ApplicantRegId,
                    VerificationStatus: VerificationStatus.value,
                    VerificationRemarks: $scope.facremarksObj.remarks,
                    ProgrammeInstancePartTermId: $scope.InstPartTermId,
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
                        
                        $localStorage.BacktoPostPage.PreSubmitFlag = true;
                        $localStorage.BacktoPostPage.PreFacultyStatus = VerificationStatus.value;
                        $localStorage.BacktoPostPage.PreFacultyRemarks = $scope.facremarksObj.remarks;
                        $localStorage.BacktoPostPage.PreIndex1 = $localStorage.PreVerificationData.map(function (item) { return item.Id; }).indexOf($localStorage.VerificationAppId);

                        $scope.backToPostConfigList();
                       
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.offSpinner();
                    }
                    else {
                        
                        $scope.offSpinner();
                        alert(response.obj);
                        $scope.EligibilityStatus = "";
                        //$scope.remarks = "";
                        $scope.FeeCategoryPartTermMapId = 0;
                        //$state.go('PostConfiguration');
                        $localStorage.BacktoPostPage.PreSubmitFlag = true;
                        $localStorage.BacktoPostPage.PreFacultyStatus = VerificationStatus.value;
                        $localStorage.BacktoPostPage.PreFacultyRemarks = $scope.facremarksObj.remarks;
                        $localStorage.BacktoPostPage.PreIndex1 = $localStorage.PreVerificationData.map(function (item) { return item.Id; }).indexOf($localStorage.VerificationAppId);

                        $scope.backToPostConfigList();
                    }
                   
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            }
        });
    };


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
                    url: 'api/PostApplicantVerification/PreVerificationUpdateFacultyMainSubmit',
                    data: {
                        Id: $localStorage.VerificationAppId,
                        ApplicantRegId: $scope.ApplicantRegId,
                        ProgrammeInstancePartTermId: $scope.InstPartTermId,
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
                url: 'api/PostApplicantVerification/PreVerificationUpdateFacultyMainSubmit',
                data: {
                    Id: $localStorage.VerificationAppId,
                    ApplicantRegId: $scope.ApplicantRegId,
                    ProgrammeInstancePartTermId: $scope.InstPartTermId,
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
        $scope.onSpinner();
        $localStorage.BacktoPostPage.Flag = true;
        $localStorage.BacktoPostPage.AcademicYearId = $scope.AcademicYearId;
        $localStorage.BacktoPostPage.ProgPTID = $scope.InstPartTermId;
        $localStorage.BacktoPostPage.VerificationStatus = $scope.localEligibilityStatus;
        $state.go('PreConfigurationForApplication');
    };

    //Function for check old and new FacultyRemarks
    $scope.CheckFacultyRemarks = function () {

        if ($scope.facremarksObj.remarks != null && $scope.facremarksObj.remarks != "" && $scope.facremarksObj.remarks != undefined) {
            var A = "";
            var B = "";
            var C = "";
            var diff = "";

            A = $scope.VerificationRemarks;

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
    
    //Function for Update NCL Certi Doc Verification from AdmApplicationRegistration
    $scope.UpdateNCLCertiVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateNCLCertiVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsNCLCertiVerified: ApplicantData.objApplicant.IsNCLCertiVerified
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
                    $scope.ObjPersonal.IsNCLCertiVerified = ApplicantData.objApplicant.IsNCLCertiVerified;
                    $scope.docName = "";
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Update EWS_IA Doc Verification from AdmApplicationRegistration
    $scope.UpdateEWS_IADocVerification = function (ApplicantData) {
        $http({
            method: 'POST',
            url: 'api/PostApplicantVerification/UpdateEWS_IADocVerification',
            data: {
                ApplicantRegId: $scope.ApplicantRegId,
                IsEWS_IADocVerified: ApplicantData.objApplicant.IsEWS_IADocVerified
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
                    $scope.ObjPersonal.IsEWS_IADocVerified = ApplicantData.objApplicant.IsEWS_IADocVerified;
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

            if (($scope.ObjPersonal.IsSocialCategoryDocVerified == 'False') && ($scope.ObjPersonal.SocialCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.SocialCategoryDoc != null)) {
                $scope.ObjPersonal.IsSocialCategoryDocVerified = "-Social Category ";
            }
            else {
                $scope.ObjPersonal.IsSocialCategoryDocVerified = "";
            }

            if (($scope.ObjPersonal.IsReservationCategoryDocVerified == 'False') && ($scope.ObjPersonal.ReservationCategoryDoc != 'Not Applicable' && $scope.ObjPersonal.ReservationCategoryDoc != null)) {
                $scope.ObjPersonal.IsReservationCategoryDocVerified = "-Reservation Category ";
            }
            else {
                $scope.ObjPersonal.IsReservationCategoryDocVerified = "";
            }

            if (($scope.ObjPersonal.IsEWSDocVerified == 'False') && ($scope.ObjPersonal.EWSDoc != 'Not Applicable' && $scope.ObjPersonal.EWSDoc != null)) {
                $scope.ObjPersonal.IsEWSDocVerified = "-EWS ";
            }
            else {
                $scope.ObjPersonal.IsEWSDocVerified = "";
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
                $scope.ObjPersonal.IsReservationCategoryDocVerified + $scope.ObjPersonal.IsEWSDocVerified + $scope.ObjPersonal.IsPCDocVerified + $scope.ObjPersonal.IsApplicantPhotoVerified + $scope.ObjPersonal.IsApplicantSignatureVerified +
                checkEduStatus + checkSubmittedDocStatus + checkAdditionalDocStatus);
    };


    /*---------------- Start Function AddOn Criteria Details----------------*/

    //Function for go back to Student Add-On Page
    $scope.backToStudentAddOn = function () {
        $state.go('PreAdmProgrammeAddOnCriteria');
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
        $state.go('PreApplicantVerification');
    };

    /*---------------- End Function AddOn Criteria Details----------------*/


    /*---------------- Start Function For Education Details----------------*/

    $scope.modifyUserFlag = false;
    $scope.DisabledFlag = false;

    $scope.ClickOnAccord = function (ind) {

        $('.show').removeClass('show');
        $('.bg-primary').removeClass('bg-primary');
        $("#divActive" + ind).addClass("bg-primary");
        $("#collapse" + ind).addClass("show");
    }

    $scope.OtherCityChange = function () {
        
        var OtherInsCity = $('#OtherInsCity option:selected').text();
        if (OtherInsCity == "Other") {

            $scope.showval = "Other";
        }
        else {

            $scope.showval = null;
        }
    };

    $("#MarkObtained").on("keyup", function () {

        var MarkOutof = $("#MarkOutof").val();
        var MarkOutofCheck = document.getElementById("MarkOutofCheck");

        var MarkObt = $("#MarkObtained").val();
        var MarkObtCheck = document.getElementById("MarkObtCheck");

        var MarkOutof2 = parseInt(MarkOutof);
        var MarkObt2 = parseInt(MarkObt);

        if (MarkObt2 > MarkOutof2) {

            MarkObtCheck.innerHTML = "Enter valid Marks";
        }
        else {

            MarkObtCheck.innerHTML = "";
            MarkOutofCheck.innerHTML = "";
        }
    });

    $("#MarkOutof").on("keyup", function () {

        var MarkOutof = $("#MarkOutof").val();
        var MarkOutofCheck = document.getElementById("MarkOutofCheck");

        var MarkObt = $("#MarkObtained").val();
        var MarkObtCheck = document.getElementById("MarkObtCheck");

        var MarkOutof2 = parseInt(MarkOutof);
        var MarkObt2 = parseInt(MarkObt);

        if (MarkObt2 > MarkOutof2) {

            MarkOutofCheck.innerHTML = "Enter valid Marks";
        }
        else {

            MarkOutofCheck.innerHTML = "";
            MarkObtCheck.innerHTML = "";
        }
    });

    $("#CGPA").on("keyup", function () {

        var CGPA = $("#CGPA").val();
        var CGPACheck = document.getElementById("CGPACheck");

        var RE = new RegExp(/^\d+(?:\.\d{2,2})?$/);

        if (RE.test(CGPA)) {

            var CGPA2 = parseFloat(CGPA);
            if (CGPA2 > 10) {

                CGPACheck.innerHTML = "Enter valid CGPA";
            }
            else {

                CGPACheck.innerHTML = "";
            }

        } else {

            CGPACheck.innerHTML = "Enter valid CGPA";
        }
    });

    $("#PercentageEquivalenceCGPA").on("keyup", function () {

        var PercentageEquivalenceCGPA = $("#PercentageEquivalenceCGPA").val();
        var PercentageEquivalenceCGPACheck = document.getElementById("PercentageEquivalenceCGPACheck");

        var RE = new RegExp(/^\d+(?:\.\d{2,2})?$/);

        if (RE.test(PercentageEquivalenceCGPA)) {

            var PercentageEquivalenceCGPA2 = parseInt(PercentageEquivalenceCGPA);
            if (PercentageEquivalenceCGPA2 > 100 || PercentageEquivalenceCGPA2 < 35) {

                PercentageEquivalenceCGPACheck.innerHTML = "Enter valid Percentage";
            }
            else {

                PercentageEquivalenceCGPACheck.innerHTML = "";
            }

        } else {

            PercentageEquivalenceCGPACheck.innerHTML = "Enter valid Percentage";
        }
    });

    $scope.image_name = "";

    $scope.clear = function () {
        angular.element("input[type='file']").val(null);
    };

    $scope.$watchGroup(['ObjEdu.MarkOutof', 'ObjEdu.MarkObtained'], function () {
        if ($scope.ObjEdu.MarkObtained !== undefined && $scope.ObjEdu.MarkOutof !== undefined && $scope.ObjEdu.MarkObtained !== 0 && $scope.ObjEdu.MarkOutof !== 0)
            $scope.ObjEdu.Percentage = (($scope.ObjEdu.MarkObtained / $scope.ObjEdu.MarkOutof) * 100).toFixed(2);
        else
            $scope.ObjEdu.Percentage = 0;
    });

    $scope.sendeligibledegree = function () {
        
        $http({
            method: 'Post',
            url: 'MSUISApi/api/AdmEligibleDegree/getAdmEligibleDegreeList',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c1List = response.obj;
                
                //if ($scope.modifyUserFlag == true) {
                //    $scope.ObjEdu.EligibleDegreeId = $scope.ObjEdu.EligibleDegreeId;
                //}
                $scope.table1 = {
                };
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.sendlanguage = function () {
        //alert("Language Name");
        $http({
            method: 'Post',
            url: 'MSUISApi/api/Language/getLanguageList',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c2List = response.obj;
                $scope.table1 = {
                };
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.sendexaminationbody = function () {

        var EligibleDegreeName = $('#EligibleDegreeName option:selected').text();
        $scope.ObjEdu.BoardDetails = EligibleDegreeName;

        $http({
            method: 'POST',
            url: 'MSUISApi/api/ExaminationBody/getExaminationBodyList',
            data: $scope.ObjEdu,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c3List = response.obj;
                $scope.table1 = {
                };
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.sendspecialization = function () {
        $http({
            method: 'Post',
            url: 'MSUISApi/api/AdmSpecializationMaster/getAdmSpecializationMasterList',
            //data: $scope.ObjEdu,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c4List = response.obj;
                $scope.table1 = {
                };
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.sendspecialization1 = function () {
        
        $http({
            method: 'Post',
            url: 'MSUISApi/api/AdmSpecializationMaster/getAdmSpecializationMaster',
            data: $scope.ObjEdu,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c4List = response.obj;
                $scope.table1 = {
                };
            })
            .error(function (res) {
                alert(res);
            });

        $scope.sendexaminationbody();
    };

    $scope.sendcity = function () {
        $http({
            method: 'Post',
            url: 'MSUISApi/api/GenCity/getGenCityList',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c5List = response.obj;
                $scope.table1 = {
                };
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.sendclass = function () {
        $http({
            method: 'Post',
            url: 'MSUISApi/api/ClassMaster/getClassMasterList',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.c6List = response.obj;
                $scope.table1 = {
                };
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.modifyEduDetailsData = function (data) {
       
        $scope.DisabledFlag = true;
        $scope.ShowAddUpdateFlag = "swUpdate";
        $scope.ObjEdu = data;

        if ($scope.ObjEdu.CityName == "Other") {

            $scope.showval = "Other";
        }
        else {

            $scope.showval = null;
        }

        
        //For Check Education Document already uploaded.
        var EduDocSplit = null;
        var EduDoc = $scope.ObjEdu.AttachDocument;
        if (EduDoc != undefined && EduDoc != "" && EduDoc != null) {

            EduDocSplit = EduDoc.split("/").pop();
        }
        if ($scope.ObjEdu.AttachDocument != null) {

            //$scope.SocialCategoryDocView = $scope.ObjPersonal.AttachDocument;
            document.getElementById("SuccessMsgEdu").innerHTML = "File already uploaded";
            $scope.SocCatDocCheck = "File already uploaded";
            //$scope.ObjPersonal.SocialCategoryDoc = null;
        }

        $scope.sendspecialization1();
        $scope.DateRangeforResultStatus();
        $scope.modifyUserFlag = true;

    }

    $scope.UploadEduFiles = function ($files) {
        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        var fileExtn = angular.lowercase(fileExtension);

        //$scope.FileNameUpload = "AttachDocument" + fileExtn;
        $scope.FileNameUpload = $scope.ObjEdu.EligibleDegreeId + "_" + $scope.ObjEdu.ExamPassYear + fileExtension;
        $scope.AttachDocumentUpload = $scope.FileNameUpload;
        //alert($scope.FileNameUpload);
        $cookies.put("EducationFileName", $scope.FileNameUpload);
        $cookies.put("AdmApplicantRegiUserName", $scope.AdmApplicantRegiUserName);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("AttachDocument").value;

            var allowedExtensions = /(\.pdf)$/i;
            const fi = document.getElementById("AttachDocument");

            if (fileCheck == "" ||
                fileCheck === undefined) {
                //alert('Please upload the file');
                document.getElementById("ErrorMsgEdu").innerHTML = "Must upload the file";
                document.getElementById("SuccessMsgEdu").innerHTML = "";

                alert("Please Must upload the file");
                return false;
            }
            else if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgEdu").innerHTML = "It only accept PDF file";
                document.getElementById("SuccessMsgEdu").innerHTML = "";
                fileCheck.value = '';
                $("#AttachDocument").val("");
                $scope.ReservationCatFileNameUpload = null;
                alert("It only accept PDF file");
                return false;
            }

            if (fi.files.length > 0) {


                var cnt = fi.files.length;

                $scope.byId(15).then(function (response) {
                    //alert("Max" + response.data.obj[0].Value);
                    $scope.maxVal = response.data.obj[0].Value;

                    for (i = 0; i < cnt; i++) {
                        const fsize = fi.files.item(i).size;

                        const file = Math.round((fsize / 1024));
                        //alert(file + "==" + $scope.maxVal);
                        /*if (file < $scope.minVal) {
                            document.getElementById("ErrorMsgEdu").innerHTML = "Please select a file above 2MB";
                            document.getElementById("SuccessMsgEWS").innerHTML = "";

                            alert("Please select a file  above 2MB")
                            return false;

                        } else */
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgEdu").innerHTML = "Please select a file below 1MB";
                            document.getElementById("SuccessMsgEdu").innerHTML = "";
                            $("#AttachDocument").val("");
                            //$scope.ResCatDocCheck = undefined;
                            $scope.AttachDocumentUpload = null;
                            alert("Please select a file below 1MB");
                            return false;

                        }
                        else {

                            Upload.upload({
                                url: 'api/PostApplicantVerification/UploadEduFiles',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                var res_code = response.data.response_code;
                                if (res_code == "201") {

                                    alert(response.data.obj);
                                    $scope.AttachDocumentUpload = null;
                                    return false;
                                }
                                $scope.Result = response.data;
                                document.getElementById("ErrorMsgEdu").innerHTML = "";
                                document.getElementById("SuccessMsgEdu").innerHTML = "File Uploaded Successfully";

                                //$scope.UploadFiles();

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;
                                    //alert(errorMsg);
                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }

                    }
                }).catch(function (res) {
                    alert(res.obj);
                });
            }
        }
    };

    //Update Edu Details from here
    $scope.UpdateEducationDetailsDataInPreVerification = function () {

        $scope.ObjEdu.ApplicantRegId = $scope.ApplicantRegId;
        $scope.ObjEdu.AdmApplicantRegiUserName = $scope.AdmApplicantRegiUserName;
       
        //$('#Edu-modal').modal('hide');
        if ($scope.ObjEdu.EligibleDegreeId === null || $scope.ObjEdu.EligibleDegreeId === undefined ||
            $scope.ObjEdu.SpecializationId == null || $scope.ObjEdu.SpecializationId === undefined ||
            $scope.ObjEdu.ExaminationBodyId == null || $scope.ObjEdu.ExaminationBodyId === undefined ||
            $scope.ObjEdu.InstituteAttended == null || $scope.ObjEdu.InstituteAttended === undefined ||
            $scope.ObjEdu.InstituteCityId == null || $scope.ObjEdu.InstituteCityId === undefined 
            //(($scope.ObjEdu.ResultStatus == null || $scope.ObjEdu.ResultStatus === undefined)&&
            //($scope.ObjEdu.res_status == null || $scope.ObjEdu.res_status === undefined))
            //$scope.ObjEdu.res_status == null || $scope.ObjEdu.res_status === undefined

        ) {
            ////$('#Edu-modal').modal('hide');
            //$mdDialog.show(
            //    $mdDialog.alert()
            //        .parent(angular.element(document.querySelector('#Edu-modal')))
            //        .clickOutsideToClose(true)
            //        .title("Error")
            //        .textContent("Please complete the form before Submit...")
            //        .ariaLabel('Alert Dialog Demo')
            //        .ok('Okay!')

            //);
            alert("Please complete the form before Submit...");
        }
        else {
            var OtherInsCity = $('#OtherInsCity option:selected').text();
            if (OtherInsCity == "Other") {

                var OtherCity = $("#OtherCity").val();
                if (OtherCity == "") {

                    alert("Please give the City detail");
                    return false;
                }
            }
            $scope.ObjEdu.CheckCity = OtherInsCity;
            
            var res_status = $('#res_status option:selected').text();
          
            if (res_status == "Result With Marksheet") {

                if (($scope.ObjEdu.NoOfTrails == null && $scope.ObjEdu.IsFirstTrial == false) ||
                    $scope.ObjEdu.ExamPassMonth == null || $scope.ObjEdu.ExamPassMonth == undefined ||
                    $scope.ObjEdu.ExamPassYear == null || $scope.ObjEdu.ExamPassYear == undefined ||
                    $scope.ObjEdu.ExamSeatNumber == null || $scope.ObjEdu.ExamSeatNumber == undefined ||
                    $scope.ObjEdu.ExamCertificateNumber == null || $scope.ObjEdu.ExamCertificateNumber == undefined ||
                    $scope.ObjEdu.TeachingLanguageId == null || $scope.ObjEdu.TeachingLanguageId == undefined
                    || (($scope.ObjEdu.AttachDocument == null || $scope.ObjEdu.AttachDocument == undefined || $scope.ObjEdu.AttachDocument == "")
                        && ($scope.FileNameUpload == null || $scope.FileNameUpload == undefined || $scope.FileNameUpload == ""))

                ) {


                    //$mdDialog.show(
                    //    $mdDialog.alert()
                    //        .parent(angular.element(document.querySelector('#sign-in-modal')))
                    //        .clickOutsideToClose(true)
                    //        .title("Error")
                    //        .textContent("Please complete the form before Submit...")
                    //        .ariaLabel('Alert Dialog Demo')
                    //        .ok('Okay!')
                    //);
                    alert("Please complete the form before Submit...");
                    return false;
                }
                var MarkObtained = $("#MarkObtained").val();
                var MarkOutof = $("#MarkOutof").val();
                var Percentage = $("#Percentage").val();
                var Grade = $("#Grade").val();
                var CGPA = $("#CGPA").val();
                var PercentageEquivalenceCGPA = $("#PercentageEquivalenceCGPA").val();
                var cl_Id = $("#cl_Id").val();

                if ((MarkObtained == "" || MarkOutof == "") && (CGPA == "" || PercentageEquivalenceCGPA == "") && Grade == "") {

                    alert("Please give the details of Marks or CGPA or Grade");
                    return false;
                }
                else {

                    if (MarkObtained != "" || MarkOutof != "") {

                        var MarkOutof2 = parseInt(MarkOutof);
                        var MarkObt2 = parseInt(MarkObtained);

                        if (MarkObt2 > MarkOutof2) {

                            return false;
                        }
                    }
                    if (CGPA != "") {

                        var RE = new RegExp(/^\d+(?:\.\d{2,2})?$/);

                        if (RE.test(CGPA)) {

                            var CGPA2 = parseFloat(CGPA);
                            if (CGPA2 > 10) {

                                alert("Please give the CGPA");
                                return false;
                            }
                        }
                        if (PercentageEquivalenceCGPA == "") {

                            alert("Please give the Percentage Equivalence CGPA");
                            return false;
                        }

                    }
                    if (PercentageEquivalenceCGPA != "") {

                        var RE = new RegExp(/^\d+(?:\.\d{2,2})?$/);

                        if (RE.test(PercentageEquivalenceCGPA)) {

                            var PercentageEquivalenceCGPA2 = parseInt(PercentageEquivalenceCGPA);
                            if (PercentageEquivalenceCGPA2 > 100 || PercentageEquivalenceCGPA2 < 35) {

                                alert("Please give the Percentage Equivalence CGPA");
                                return false;
                            }
                        }
                        if (CGPA == "") {

                            alert("Please give the CGPA");
                            return false;
                        }
                    }
                    //debugger;
                    if (Percentage != "" && Percentage != "NaN" && Percentage != "Infinity" || PercentageEquivalenceCGPA != "") {

                        if (cl_Id == "") {

                            alert("Please give the Class details");
                            return false;
                        }
                    }
                    if ($scope.ObjEdu.IsFirstTrial == null || $scope.ObjEdu.IsFirstTrial == undefined ||
                        $scope.ObjEdu.IsLastQualifyingExam == null || $scope.ObjEdu.IsLastQualifyingExam == undefined) {

                        alert("Please complete the form before Submit...");
                        return false;
                    }
                }
            }
            else {

                var IsDeclare = $('#IsDeclare').is(":checked");
                if (IsDeclare == false) {

                    alert("Please submit your declaration");
                    return false;
                }
            }

            
            //if ($scope.FileNameUpload == null || $scope.FileNameUpload == undefined || $scope.FileNameUpload == "" || $scope.FileNameUpload == "[object Object]") {
            //    $scope.ObjEdu.AttachDocumentNew = $scope.ObjEdu.AttachDocument;
            //}


            //else {
            //    $scope.ObjEdu.AttachDocumentNew = $scope.ObjEdu.AdmApplicantRegiUserName + '_' + $scope.FileNameUpload;
            //}

            //===============Check document value for NCLCerti===============
            if (($scope.AttachDocumentUpload == "" || $scope.AttachDocumentUpload == undefined || $scope.AttachDocumentUpload == null) && ($scope.ObjEdu.AttachDocument == null || $scope.ObjEdu.AttachDocument == "")) {
                $scope.ObjEdu.AttachDocument = null;
            }
            else if (($scope.AttachDocumentUpload == "" || $scope.AttachDocumentUpload == undefined || $scope.AttachDocumentUpload == null) && ($scope.ObjEdu.AttachDocument != null && $scope.ObjEdu.AttachDocument != "")) {
                var EduDocSplit = null;
                var EduD = $scope.ObjEdu.AttachDocument;
                if (EduD != undefined && EduD != "" && EduD != null) {

                    EduDocSplit = EduD.split("/").pop();
                }
                $scope.ObjEdu.AttachDocumentNew = EduDocSplit;
            }
            else if ($scope.AttachDocumentUpload != "" || $scope.AttachDocumentUpload != undefined || $scope.AttachDocumentUpload != null) {
                $scope.ObjEdu.AttachDocumentNew = $scope.ObjEdu.AdmApplicantRegiUserName + '_' + $scope.AttachDocumentUpload;
            }


            if (res_status == "Result With Marksheet" && $scope.ObjEdu.IsFirstTrial == true) {
                $scope.ObjEdu.NoOfTrails = null;
            }

            $http({
                method: 'POST',
                url: 'api/PostApplicantVerification/UpdateEducationDetailsDataInPreVerification',
                data: $scope.ObjEdu,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error");
                        alert(response.obj);
                        //$scope.getEduDetailsList();
                        $scope.getPostApplicantConfig();
                        $('#Edu-modal').modal('hide');
                    }
                    else {
                        alert(response.obj);
                        //$scope.clear();
                        //$scope.ObjEdu = {};
                        //$scope.getEduDetailsList();
                        //$scope.FileNameUpload = {};
                        $scope.getPostApplicantConfig();
                        $('#Edu-modal').modal('hide');
                        //$scope.modifyUserFlag = false;
                        //$scope.DisabledFlag = false;


                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*---------------- End Function For Education Details----------------*/


    /*---------------- Start Function For Personal Details----------------*/

    $scope.modifyPersonalDetailsData = function (data) {

        $scope.DisabledFlag = true;
        $scope.ShowAddUpdateFlag = "swUpdate";
        $scope.ObjPersonal = data;

        var IsuueDate = $scope.ObjPersonal.NCLCerti_IsuueDateView.split("-");
        $scope.ObjPersonal.NCLCerti_IsuueDate = new Date(IsuueDate[2], (IsuueDate[1] >= 1) ? (IsuueDate[1] - 1) : IsuueDate[1], IsuueDate[0]);

        var ValidityDate = $scope.ObjPersonal.NCLCertiValidityDateView.split("-");
        $scope.ObjPersonal.NCLCertiValidityDate = new Date(ValidityDate[2], (ValidityDate[1] >= 1) ? (ValidityDate[1] - 1) : ValidityDate[1], ValidityDate[0]);

        var EWS_IsuueDate = $scope.ObjPersonal.EWSCerti_IsuueDateView.split("-");
        $scope.ObjPersonal.EWSCerti_IsuueDate = new Date(EWS_IsuueDate[2], (EWS_IsuueDate[1] >= 1) ? (EWS_IsuueDate[1] - 1) : EWS_IsuueDate[1], EWS_IsuueDate[0]);

        var EWS_ValidityDate = $scope.ObjPersonal.EWSCertiValidityDateView.split("-");
        $scope.ObjPersonal.EWSCertiValidityDate = new Date(EWS_ValidityDate[2], (EWS_ValidityDate[1] >= 1) ? (EWS_ValidityDate[1] - 1) : EWS_ValidityDate[1], EWS_ValidityDate[0]);

        $scope.modifyUserFlag = true;

    }

    //For Social Reservation Catagory
    $scope.EnableSocialCategory = function () {
        if (document.getElementById('ChSocialYes').checked) {
            document.getElementById('ifSocialYes').style.display = '';
            document.getElementById('ifSocialYes1').style.display = '';

        }
        else {
            document.getElementById('ifSocialYes').style.display = 'none';
            document.getElementById('ifSocialYes1').style.display = 'none';
        }
    };

    $scope.SocialCategoryMasterGet = function () {
        //alert("test1");

        //$scope.TestState = data;
        $http({
            method: 'GET',
            url: 'MSUISApi/api/StudentRegistration/MstSocialCategoryGet',
            data: $scope.ObjPersonal,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.scList = response.obj;
                //alert("test2");
                //$scope.TestState = {
                //};
            })
            .error(function (res) {
                alert(res);
            });

    };

    $scope.sendApp = function () {

        $http({
            method: 'GET',
            url: 'MSUISApi/api/StudentRegistration/GenReservationCategoryGet',
            data: $scope.ObjPersonal,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AppList = response.obj;
                console.log($scope.AppList);
            })
            .error(function (res) {
                alert(res);
            });
    };

    //For Social Reservation Catagory
    $scope.EnableDisableEWS = function () {
        if (document.getElementById('chkYes').checked) {
            document.getElementById('iYes').style.display = '';
            document.getElementById('iYes1').style.display = '';
            document.getElementById('iYes2').style.display = '';
            document.getElementById('iYes3').style.display = '';
        }
        else {
            document.getElementById('iYes').style.display = 'none';
            document.getElementById('iYes1').style.display = 'none';
            document.getElementById('iYes2').style.display = 'none';
            document.getElementById('iYes3').style.display = 'none';
        }

        if (document.getElementById('chkNo').checked) {
            //$scope.ApplicantRegistration.EWSDoc = "Not Applicable";
        } else {

            document.getElementById('iYes').style.display = '';
            document.getElementById('iYes1').style.display = '';
            document.getElementById('iYes2').style.display = '';
            document.getElementById('iYes3').style.display = '';
        }
    };

    $scope.UploadEWSFiles = function ($files) {
        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        var fileExtn = angular.lowercase(fileExtension);

        $scope.FileNameUpload = "EWS" + fileExtn;
        $scope.EWSFileNameUpload = $scope.FileNameUpload;
        //alert($scope.FileNameUpload);
        $cookies.put("PersonalDocs", $scope.FileNameUpload);
        $cookies.put("AdmApplicantRegiUserName", $scope.AdmApplicantRegiUserName);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("EWS_Doc").value;

            var allowedExtensions = /(\.pdf)$/i;
            const fi = document.getElementById("EWS_Doc");

            if (fileCheck == "" ||
                fileCheck === undefined) {
                //alert('Please upload the file');
                document.getElementById("ErrorMsgEWS").innerHTML = "Must upload the file";
                document.getElementById("SuccessMsgEWS").innerHTML = "";

                alert("Please Must upload the file");
                return false;
            }
            else if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgEWS").innerHTML = "It only accept PDF file";
                document.getElementById("SuccessMsgEWS").innerHTML = "";
                fileCheck.value = '';
                $("#EWS_Doc").val("");
                $scope.EWSFileNameUpload = null;
                alert("It only accept PDF file");
                return false;
            }

            if (fi.files.length > 0) {


                var cnt = fi.files.length;

                $scope.byId(16).then(function (response) {
                    //alert("Max" + response.data.obj[0].Value);
                    $scope.maxVal = response.data.obj[0].Value;

                    for (i = 0; i < cnt; i++) {
                        const fsize = fi.files.item(i).size;

                        const file = Math.round((fsize / 1024));
                        //alert(file + "==" + $scope.maxVal);
                        /*if (file < $scope.minVal) {
                            document.getElementById("ErrorMsgEWS").innerHTML = "Please select a file above 2MB";
                            document.getElementById("SuccessMsgEWS").innerHTML = "";

                            alert("Please select a file  above 2MB")
                            return false;

                        } else */
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgEWS").innerHTML = "Please select a file below 1MB";
                            document.getElementById("SuccessMsgEWS").innerHTML = "";
                            $("#EWS_Doc").val("");
                            //$scope.EWSDocCheck = undefined;
                            $scope.EWSFileNameUpload = null;
                            alert("Please select a file below 1MB");
                            return false;

                        }
                        else {

                            Upload.upload({
                                url: 'api/PostApplicantVerification/UploadPersonalDocs',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                var res_code = response.data.response_code;
                                if (res_code == "201") {

                                    alert(response.data.obj);
                                    $scope.EWSFileNameUpload = null;
                                    return false;
                                }
                                $scope.Result = response.data;
                                $scope.EWSDocView = response.data.obj;
                                document.getElementById("ErrorMsgEWS").innerHTML = "";
                                document.getElementById("SuccessMsgEWS").innerHTML = "File Uploaded Successfully";

                                $scope.UploadFiles();

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;
                                    //alert(errorMsg);
                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }

                    }
                }).catch(function (res) {
                    alert(res.obj);
                });
            }
        }
    };

    $scope.UploadSocialCatFiles = function ($files) {
        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        var fileExtn = angular.lowercase(fileExtension);

        $scope.FileNameUpload = "SocialCat" + fileExtn;
        $scope.SocialCatFileNameUpload = $scope.FileNameUpload;
        //alert($scope.FileNameUpload);
        $cookies.put("PersonalDocs", $scope.FileNameUpload);
        $cookies.put("AdmApplicantRegiUserName", $scope.AdmApplicantRegiUserName);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("SocialCat").value;

            var allowedExtensions = /(\.pdf)$/i;
            const fi = document.getElementById("SocialCat");

            if (fileCheck == "" ||
                fileCheck === undefined) {
                //alert('Please upload the file');
                document.getElementById("ErrorMsgSocialCat").innerHTML = "Must upload the file";
                document.getElementById("SuccessMsgSocialCat").innerHTML = "";

                alert("Please Must upload the file");
                return false;
            }
            else if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgSocialCat").innerHTML = "It only accept PDF file";
                document.getElementById("SuccessMsgSocialCat").innerHTML = "";
                fileCheck.value = '';
                $("#SocialCat").val("");
                $scope.SocialCatFileNameUpload = null;
                alert("It only accept PDF file");
                return false;
            }

            if (fi.files.length > 0) {


                var cnt = fi.files.length;

                $scope.byId(14).then(function (response) {
                    //alert("Max" + response.data.obj[0].Value);
                    $scope.maxVal = response.data.obj[0].Value;

                    for (i = 0; i < cnt; i++) {
                        const fsize = fi.files.item(i).size;

                        const file = Math.round((fsize / 1024));
                        //alert(file + "==" + $scope.maxVal);
                        /*if (file < $scope.minVal) {
                            document.getElementById("ErrorMsgEWS").innerHTML = "Please select a file above 2MB";
                            document.getElementById("SuccessMsgEWS").innerHTML = "";

                            alert("Please select a file  above 2MB")
                            return false;

                        } else */
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgSocialCat").innerHTML = "Please select a file below 1MB";
                            document.getElementById("SuccessMsgSocialCat").innerHTML = "";
                            $("#SocialCat").val("");
                            //$scope.SocCatDocCheck = undefined;
                            $scope.SocialCatFileNameUpload = null;
                            alert("Please select a file below 1MB");
                            return false;

                        }
                        else {

                            Upload.upload({
                                url: 'api/PostApplicantVerification/UploadPersonalDocs',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                var res_code = response.data.response_code;
                                if (res_code == "201") {

                                    alert(response.data.obj);
                                    $scope.SocialCatFileNameUpload = null;
                                    return false;
                                }
                                $scope.Result = response.data;
                                $scope.SocialCategoryDocView = response.data.obj;
                                document.getElementById("ErrorMsgSocialCat").innerHTML = "";
                                document.getElementById("SuccessMsgSocialCat").innerHTML = "File Uploaded Successfully";

                                $scope.UploadFiles();

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;
                                    //alert(errorMsg);
                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }

                    }
                }).catch(function (res) {
                    alert(res.obj);
                });
            }
        }
    };

    $scope.UploadReservationCatFiles = function ($files) {
        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        var fileExtn = angular.lowercase(fileExtension);

        $scope.FileNameUpload = "Reservation" + fileExtn;
        $scope.ReservationCatFileNameUpload = $scope.FileNameUpload;
        //alert($scope.FileNameUpload);
        $cookies.put("PersonalDocs", $scope.FileNameUpload);
        $cookies.put("AdmApplicantRegiUserName", $scope.AdmApplicantRegiUserName);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("ReservationCat").value;

            var allowedExtensions = /(\.pdf)$/i;
            const fi = document.getElementById("ReservationCat");

            if (fileCheck == "" ||
                fileCheck === undefined) {
                //alert('Please upload the file');
                document.getElementById("ErrorMsgReservationCat").innerHTML = "Must upload the file";
                document.getElementById("SuccessMsgReservationCat").innerHTML = "";

                alert("Please Must upload the file");
                return false;
            }
            else if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgReservationCat").innerHTML = "It only accept PDF file";
                document.getElementById("SuccessMsgReservationCat").innerHTML = "";
                fileCheck.value = '';
                $("#ReservationCat").val("");
                $scope.ReservationCatFileNameUpload = null;
                alert("It only accept PDF file");
                return false;
            }

            if (fi.files.length > 0) {


                var cnt = fi.files.length;

                $scope.byId(15).then(function (response) {
                    //alert("Max" + response.data.obj[0].Value);
                    $scope.maxVal = response.data.obj[0].Value;

                    for (i = 0; i < cnt; i++) {
                        const fsize = fi.files.item(i).size;

                        const file = Math.round((fsize / 1024));
                        //alert(file + "==" + $scope.maxVal);
                        /*if (file < $scope.minVal) {
                            document.getElementById("ErrorMsgEWS").innerHTML = "Please select a file above 2MB";
                            document.getElementById("SuccessMsgEWS").innerHTML = "";

                            alert("Please select a file  above 2MB")
                            return false;

                        } else */
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgReservationCat").innerHTML = "Please select a file below 1MB";
                            document.getElementById("SuccessMsgReservationCat").innerHTML = "";
                            $("#ReservationCat").val("");
                            //$scope.ResCatDocCheck = undefined;
                            $scope.ReservationCatFileNameUpload = null;
                            alert("Please select a file below 1MB");
                            return false;

                        }
                        else {

                            Upload.upload({
                                url: 'api/PostApplicantVerification/UploadPersonalDocs',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                var res_code = response.data.response_code;
                                if (res_code == "201") {

                                    alert(response.data.obj);
                                    $scope.ReservationCatFileNameUpload = null;
                                    return false;
                                }
                                $scope.Result = response.data;
                                $scope.ResCatDocView = response.data.obj;
                                document.getElementById("ErrorMsgReservationCat").innerHTML = "";
                                document.getElementById("SuccessMsgReservationCat").innerHTML = "File Uploaded Successfully";

                                $scope.UploadFiles();

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;
                                    //alert(errorMsg);
                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }

                    }
                }).catch(function (res) {
                    alert(res.obj);
                });
            }
        }
    };

    $scope.UploadNCLCerti = function ($files) {
        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        var fileExtn = angular.lowercase(fileExtension);

        $scope.FileNameUpload = "NCLCerti" + fileExtn;
        $scope.NCLCertiUpload = $scope.FileNameUpload;
        //alert($scope.FileNameUpload);
        $cookies.put("PersonalDocs", $scope.FileNameUpload);
        $cookies.put("AdmApplicantRegiUserName", $scope.AdmApplicantRegiUserName);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("NCLCerti").value;

            var allowedExtensions = /(\.pdf)$/i;
            const fi = document.getElementById("NCLCerti");

            if (fileCheck == "" ||
                fileCheck === undefined) {
                //alert('Please upload the file');
                document.getElementById("ErrorMsgNCLCerti").innerHTML = "Must upload the file";
                document.getElementById("SuccessMsgNCLCerti").innerHTML = "";

                alert("Please Must upload the file");
                return false;
            }
            else if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgNCLCerti").innerHTML = "It only accept PDF file";
                document.getElementById("SuccessMsgNCLCerti").innerHTML = "";
                fileCheck.value = '';
                $("#NCLCerti").val("");
                $scope.ReservationCatFileNameUpload = null;
                alert("It only accept PDF file");
                return false;
            }

            if (fi.files.length > 0) {


                var cnt = fi.files.length;

                $scope.byId(15).then(function (response) {
                    //alert("Max" + response.data.obj[0].Value);
                    $scope.maxVal = response.data.obj[0].Value;

                    for (i = 0; i < cnt; i++) {
                        const fsize = fi.files.item(i).size;

                        const file = Math.round((fsize / 1024));
                        //alert(file + "==" + $scope.maxVal);
                        /*if (file < $scope.minVal) {
                            document.getElementById("ErrorMsgEWS").innerHTML = "Please select a file above 2MB";
                            document.getElementById("SuccessMsgEWS").innerHTML = "";

                            alert("Please select a file  above 2MB")
                            return false;

                        } else */
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgNCLCerti").innerHTML = "Please select a file below 1MB";
                            document.getElementById("SuccessMsgNCLCerti").innerHTML = "";
                            $("#NCLCerti").val("");
                            //$scope.ResCatDocCheck = undefined;
                            $scope.ReservationCatFileNameUpload = null;
                            alert("Please select a file below 1MB");
                            return false;

                        }
                        else {

                            Upload.upload({
                                url: 'api/PostApplicantVerification/UploadPersonalDocs',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                var res_code = response.data.response_code;
                                if (res_code == "201") {

                                    alert(response.data.obj);
                                    $scope.NCLCertiFileNameUpload = null;
                                    return false;
                                }
                                $scope.Result = response.data;
                                $scope.NCLCertiView = response.data.obj;
                                document.getElementById("ErrorMsgNCLCerti").innerHTML = "";
                                document.getElementById("SuccessMsgNCLCerti").innerHTML = "File Uploaded Successfully";

                                $scope.UploadFiles();

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;
                                    //alert(errorMsg);
                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }

                    }
                }).catch(function (res) {
                    alert(res.obj);
                });
            }
        }
    };

    $scope.UploadEWS_IAFiles = function ($files) {
        $scope.SelectedFiles = $files;

        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        var fileExtn = angular.lowercase(fileExtension);

        $scope.FileNameUpload = "EWS_IADoc" + fileExtn;
        $scope.EWS_IADocUpload = $scope.FileNameUpload;
        //alert($scope.FileNameUpload);
        $cookies.put("PersonalDocs", $scope.FileNameUpload);
        $cookies.put("AdmApplicantRegiUserName", $scope.AdmApplicantRegiUserName);

        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var fileCheck = document.getElementById("EWS_IADoc").value;

            var allowedExtensions = /(\.pdf)$/i;
            const fi = document.getElementById("EWS_IADoc");

            if (fileCheck == "" ||
                fileCheck === undefined) {
                //alert('Please upload the file');
                document.getElementById("ErrorMsgEWS_IADoc").innerHTML = "Must upload the file";
                document.getElementById("SuccessMsgEWS_IADoc").innerHTML = "";

                alert("Please Must upload the file");
                return false;
            }
            else if (!allowedExtensions.exec(fileCheck)) {

                document.getElementById("ErrorMsgEWS_IADoc").innerHTML = "It only accept PDF file";
                document.getElementById("SuccessMsgEWS_IADoc").innerHTML = "";
                fileCheck.value = '';
                $("#EWS_IADoc").val("");
                $scope.EWS_IADocUpload = null;
                alert("It only accept PDF file");
                return false;
            }

            if (fi.files.length > 0) {


                var cnt = fi.files.length;

                $scope.byId(15).then(function (response) {
                    //alert("Max" + response.data.obj[0].Value);
                    $scope.maxVal = response.data.obj[0].Value;

                    for (i = 0; i < cnt; i++) {
                        const fsize = fi.files.item(i).size;

                        const file = Math.round((fsize / 1024));
                        //alert(file + "==" + $scope.maxVal);
                        /*if (file < $scope.minVal) {
                            document.getElementById("ErrorMsgEWS").innerHTML = "Please select a file above 2MB";
                            document.getElementById("SuccessMsgEWS").innerHTML = "";

                            alert("Please select a file  above 2MB")
                            return false;

                        } else */
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgEWS_IADoc").innerHTML = "Please select a file below 1MB";
                            document.getElementById("SuccessMsgEWS_IADoc").innerHTML = "";
                            $("#EWS_IADoc").val("");
                            //$scope.ResCatDocCheck = undefined;
                            $scope.EWS_IADocUpload = null;
                            alert("Please select a file below 1MB");
                            return false;

                        }
                        else {

                            Upload.upload({
                                url: 'api/PostApplicantVerification/UploadPersonalDocs',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {

                                var res_code = response.data.response_code;
                                if (res_code == "201") {

                                    alert(response.data.obj);
                                    $scope.EWS_IADocUpload = null;
                                    return false;
                                }
                                $scope.Result = response.data;
                                $scope.EWS_IAView = response.data.obj;
                                document.getElementById("ErrorMsgEWS_IADoc").innerHTML = "";
                                document.getElementById("SuccessMsgEWS_IADoc").innerHTML = "File Uploaded Successfully";

                                $scope.UploadFiles();

                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;
                                    //alert(errorMsg);
                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }

                    }
                }).catch(function (res) {
                    alert(res.obj);
                });
            }
        }
    };
   
    //Function for Update Personal Details
    $scope.UpdatePersonalDetailsDataInPreVerification = function () {
       
        $scope.ObjPersonal.AdmApplicantRegiUserName = $scope.AdmApplicantRegiUserName;
        $scope.ObjPersonal.ApplicantRegId = $scope.ApplicantRegId;

       //===============Check document value for SocialCategoryDoc===============
        if (($scope.SocialCatFileNameUpload == "" || $scope.SocialCatFileNameUpload == undefined || $scope.SocialCatFileNameUpload == null) && ($scope.ObjPersonal.SocialCategoryDoc == null || $scope.ObjPersonal.SocialCategoryDoc == "")) {
            $scope.ObjPersonal.SocialCategoryDoc = null;
        }
        else if (($scope.SocialCatFileNameUpload == "" || $scope.SocialCatFileNameUpload == undefined || $scope.SocialCatFileNameUpload == null) && ($scope.ObjPersonal.SocialCategoryDoc != null && $scope.ObjPersonal.SocialCategoryDoc != "")) {
            var SocialDocSplit = null;
            var scDocument = $scope.ObjPersonal.SocialCategoryDoc;
            if (scDocument != undefined && scDocument != "" && scDocument != null) {

                SocialDocSplit = scDocument.split("_").pop();
            }
            $scope.ObjPersonal.SocialCategoryDoc = SocialDocSplit;
        }
        else if ($scope.SocialCatFileNameUpload != "" || $scope.SocialCatFileNameUpload != undefined || $scope.SocialCatFileNameUpload != null) {
            $scope.ObjPersonal.SocialCategoryDoc = $scope.SocialCatFileNameUpload;
        }

        //===============Check document value for ReservationCategoryDoc===============
        if (($scope.ReservationCatFileNameUpload == "" || $scope.ReservationCatFileNameUpload == undefined || $scope.ReservationCatFileNameUpload == null) && ($scope.ObjPersonal.ReservationCategoryDoc == null || $scope.ObjPersonal.ReservationCategoryDoc == "")) {
            $scope.ObjPersonal.ReservationCategoryDoc = null;
        }
        else if (($scope.ReservationCatFileNameUpload == "" || $scope.ReservationCatFileNameUpload == undefined || $scope.ReservationCatFileNameUpload == null) && ($scope.ObjPersonal.ReservationCategoryDoc != null && $scope.ObjPersonal.ReservationCategoryDoc != "")) {
            var ResDocSplit = null;
            var ResD = $scope.ObjPersonal.ReservationCategoryDoc;
            if (ResD != undefined && ResD != "" && ResD != null) {

                ResDocSplit = ResD.split("_").pop();
            }
            $scope.ObjPersonal.ReservationCategoryDoc = ResDocSplit;
        }
        else if ($scope.ReservationCatFileNameUpload != "" || $scope.ReservationCatFileNameUpload != undefined || $scope.ReservationCatFileNameUpload != null) {
            $scope.ObjPersonal.ReservationCategoryDoc = $scope.ReservationCatFileNameUpload;
        }

        //===============Check document value for EWSDoc===============
        if (($scope.EWSFileNameUpload == "" || $scope.EWSFileNameUpload == undefined || $scope.EWSFileNameUpload == null) && ($scope.ObjPersonal.EWSDoc == null || $scope.ObjPersonal.EWSDoc == "")) {
            $scope.ObjPersonal.EWSDoc = null;
        }
        else if (($scope.EWSFileNameUpload == "" || $scope.EWSFileNameUpload == undefined || $scope.EWSFileNameUpload == null) && ($scope.ObjPersonal.EWSDoc != null && $scope.ObjPersonal.EWSDoc != "")) {
            var EWSDocSplit = null;
            var EWSD = $scope.ObjPersonal.EWSDoc;
            if (EWSD != undefined && EWSD != "" && EWSD != null) {

                EWSDocSplit = EWSD.split("_").pop();
            }
            $scope.ObjPersonal.EWSDoc = EWSDocSplit;
        }
        else if ($scope.EWSFileNameUpload != "" || $scope.EWSFileNameUpload != undefined || $scope.EWSFileNameUpload != null) {
            $scope.ObjPersonal.EWSDoc = $scope.EWSFileNameUpload;
        }

        //===============Check document value for NCLCerti===============
        if (($scope.NCLCertiUpload == "" || $scope.NCLCertiUpload == undefined || $scope.NCLCertiUpload == null) && ($scope.ObjPersonal.NCLCerti == null || $scope.ObjPersonal.NCLCerti == "")) {
            $scope.ObjPersonal.NCLCerti = null;
        }
        else if (($scope.NCLCertiUpload == "" || $scope.NCLCertiUpload == undefined || $scope.NCLCertiUpload == null) && ($scope.ObjPersonal.NCLCerti != null && $scope.ObjPersonal.NCLCerti != "")) {
            var NCLDocSplit = null;
            var NCLD = $scope.ObjPersonal.NCLCerti;
            if (NCLD != undefined && NCLD != "" && NCLD != null) {

                NCLDocSplit = NCLD.split("_").pop();
            }
            $scope.ObjPersonal.NCLCerti = NCLDocSplit;
        }
        else if ($scope.NCLCertiUpload != "" || $scope.NCLCertiUpload != undefined || $scope.NCLCertiUpload != null) {
            $scope.ObjPersonal.NCLCerti = $scope.NCLCertiUpload;
        }
        
        //===============Check document value for EWS_IA Doc===============
        if (($scope.EWS_IADocUpload == "" || $scope.EWS_IADocUpload == undefined || $scope.EWS_IADocUpload == null) && ($scope.ObjPersonal.EWS_IADoc == null || $scope.ObjPersonal.EWS_IADoc == "")) {
            $scope.ObjPersonal.EWS_IADoc = null;
        }
        else if (($scope.EWS_IADocUpload == "" || $scope.EWS_IADocUpload == undefined || $scope.EWS_IADocUpload == null) && ($scope.ObjPersonal.EWS_IADoc != null && $scope.ObjPersonal.EWS_IADoc != "")) {
            var EWS_IADocSplit = null;
            var EWS_IA = $scope.ObjPersonal.EWS_IADoc;
            if (EWS_IA != undefined && EWS_IA != "" && EWS_IA != null) {

                EWS_IADocSplit = EWS_IA.split("_").pop();
            }
            $scope.ObjPersonal.EWS_IADoc = "EWS_" + EWS_IADocSplit;
        }
        else if ($scope.EWS_IADocUpload != "" || $scope.EWS_IADocUpload != undefined || $scope.EWS_IADocUpload != null) {
            $scope.ObjPersonal.EWS_IADoc = $scope.EWS_IADocUpload;
        }
        
        if ($scope.ObjPersonal.ApplicationCategoryId == 1) {
            $scope.ObjPersonal.NCLCerti = null;
            $scope.ObjPersonal.NCLCertiNo = null;
            $scope.ObjPersonal.NCLCerti_IsuueDate = null;
            $scope.ObjPersonal.NCLCertiValidityDate = null;
            $scope.ObjPersonal.IsEWS = null;
            $scope.ObjPersonal.EWSDoc = null;
            $scope.ObjPersonal.EWSCertiNo = null;
            $scope.ObjPersonal.EWSCerti_IsuueDate = null;
            $scope.ObjPersonal.EWSCertiValidityDate = null;
            $scope.ObjPersonal.EWS_IADoc = null;
        }
        else if ($scope.ObjPersonal.ApplicationCategoryId == 2) {
            $scope.ObjPersonal.NCLCerti = null;
            $scope.ObjPersonal.NCLCertiNo = null;
            $scope.ObjPersonal.NCLCerti_IsuueDate = null;
            $scope.ObjPersonal.NCLCertiValidityDate = null;
            $scope.ObjPersonal.IsEWS = null;
            $scope.ObjPersonal.EWSDoc = null;
            $scope.ObjPersonal.EWSCertiNo = null;
            $scope.ObjPersonal.EWSCerti_IsuueDate = null;
            $scope.ObjPersonal.EWSCertiValidityDate = null;
            $scope.ObjPersonal.EWS_IADoc = null;
        }
        else if ($scope.ObjPersonal.ApplicationCategoryId == 3) {
            $scope.ObjPersonal.ReservationCategoryDoc = null;
            $scope.ObjPersonal.NCLCerti = null;
            $scope.ObjPersonal.NCLCertiNo = null;
            $scope.ObjPersonal.NCLCerti_IsuueDate = null;
            $scope.ObjPersonal.NCLCertiValidityDate = null;
        }
        else if ($scope.ObjPersonal.ApplicationCategoryId == 4) {
            $scope.ObjPersonal.IsEWS = null;
            $scope.ObjPersonal.EWSDoc = null;
            $scope.ObjPersonal.EWSCertiNo = null;
            $scope.ObjPersonal.EWSCerti_IsuueDate = null;
            $scope.ObjPersonal.EWSCertiValidityDate = null;
            $scope.ObjPersonal.EWS_IADoc = null;
        }
        
        if ($scope.ObjPersonal.SocialCategoryId == 16) {
            $scope.ObjPersonal.SocialCategoryDoc = null;
        }
        if ($scope.ObjPersonal.myDropDownSC == "False") {
            $scope.ObjPersonal.SocialCategoryId = null;
            $scope.ObjPersonal.SocialCategoryDoc = null;
        }
        if ($scope.ObjPersonal.ApplicationCategoryId == 3 && $scope.ObjPersonal.IsEWS == "False") {
            $scope.ObjPersonal.EWSDoc = null;
            $scope.ObjPersonal.EWSCertiNo = null;
            $scope.ObjPersonal.EWSCerti_IsuueDate = null;
            $scope.ObjPersonal.EWSCertiValidityDate = null;
            $scope.ObjPersonal.EWS_IADoc = null;
        }
        if ($scope.ObjPersonal.ApplicationCategoryId == 3 && $scope.ObjPersonal.IsEWS == "True" && $scope.ObjPersonal.EWS_IADoc == null){
            $scope.ObjPersonal.EWS_IADoc = null;
}

        if (($scope.ObjPersonal.myDropDownSC == "True") && ($scope.ObjPersonal.SocialCategoryId == null)) {
            alert("Please Select Social Category..!!")
        }
        else if (($scope.ObjPersonal.SocialCategoryId != 16 && $scope.ObjPersonal.myDropDownSC == "True") && ($scope.ObjPersonal.SocialCategoryDoc == null)) {
            alert("Please Add Social Category Document..!!")
        }
        else if (($scope.ObjPersonal.ApplicationCategoryId == 1 || $scope.ObjPersonal.ApplicationCategoryId == 2 || $scope.ObjPersonal.ApplicationCategoryId == 4) && ($scope.ObjPersonal.ReservationCategoryDoc == null)) {
            alert("Please Add Reservation Category Document..!!")
        }
        else if (($scope.ObjPersonal.ApplicationCategoryId == 4) && ($scope.ObjPersonal.NCLCerti == null || $scope.ObjPersonal.NCLCertiNo == null || $scope.ObjPersonal.NCLCerti_IsuueDate == null || $scope.ObjPersonal.NCLCertiValidityDate == null)) {
            alert("Please Add All Details For Non Creamy Layer Certificate..!!")
        }
        else if (($scope.ObjPersonal.ApplicationCategoryId == 3) && ($scope.ObjPersonal.IsEWS == null)) {
            alert("Please select belong to EWS or not?")
        }
        else if (($scope.ObjPersonal.ApplicationCategoryId == 3) && ($scope.ObjPersonal.IsEWS == "True") && ($scope.ObjPersonal.EWSDoc == null)) {
            alert("Please Add EWS Document..!!")
        }
        else if (($scope.ObjPersonal.ApplicationCategoryId == 3) && ($scope.ObjPersonal.IsEWS == "True") && ($scope.ObjPersonal.EWSCertiNo == null)) {
            alert("Please Add EWS Certificate Number..!!")
        }
        else if (($scope.ObjPersonal.ApplicationCategoryId == 3) && ($scope.ObjPersonal.IsEWS == "True") && ($scope.ObjPersonal.EWSCerti_IsuueDate == null)) {
            alert("Please Add EWS Certificate Isuue Date..!!")
        }
        else if (($scope.ObjPersonal.ApplicationCategoryId == 3) && ($scope.ObjPersonal.IsEWS == "True") && ($scope.ObjPersonal.EWSCertiValidityDate == null)) {
            alert("Please Add EWS Certificate Validity Date..!!")
        }
        else {
            $http({
                method: 'POST',
                url: 'api/PostApplicantVerification/UpdatePersonalDetailsDataInPreVerification',
                data: $scope.ObjPersonal,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error");
                        alert(response.obj);
                        $scope.getPostApplicantConfig();
                        $('#Personal-modal').modal('hide');
                    }
                    else {
                        alert(response.obj);
                        $scope.getPostApplicantConfig();

                        $('#Personal-modal').modal('hide');



                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*---------------- End Function For Personal Details----------------*/

});


app.directive('ngFiles', ['$parse', function ($parse) {

    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFiles);
        element.on('change', function (event) {
            onChange(scope, { $files: event.target.files });
        });
    };

    return {
        link: fn_link
    }
}])


app.directive('monthOptions', function () {
    return {
        restrict: 'A',
        template:
            '<option value="">Select Month</option>' +
            '<option value="Jan">January</option>' +
            '<option value="Feb">February</option>' +
            '<option value="Mar">March</option>' +
            '<option value="Apr">April</option>' +
            '<option value="May">May</option>' +
            '<option value="Jun">June</option>' +
            '<option value="Jul">July</option>' +
            '<option value="Aug">August</option>' +
            '<option value="Sep">September</option>' +
            '<option value="Oct">October</option>' +
            '<option value="Nov">November</option>' +
            '<option value="Nov">December</option>'
    }
});

app.directive('resultstatusOptions', function () {
    return {
        restrict: 'A',
        template:
            '<option value="">-- Select Result Status --</option>' +
            '<option value="Result_With_Marksheet">Result With Marksheet</option>' 
            //'<option value="Result_Without_Marksheet">Result Without Marksheet</option>' +
            //'<option value="Result_Pending">Result Pending</option>'
    }
});

app.directive('resultpendingstatusOptions', function () {
    return {
        restrict: 'A',
        template:
            '<option value="">-- Select Result Status --</option>' +
            '<option value="Result_With_Marksheet">Result With Marksheet</option>'
            //'<option value="Result_Without_Marksheet">Result Without Marksheet</option>' 
            + '<option value="Result_Pending">Result Pending</option>'
    }
});