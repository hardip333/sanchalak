app.controller('ApplicantsAdmissionInformationDetailsCtrl', function ($scope, $http, $rootScope, $state, $cookies, Upload, $mdDialog, $localStorage, NgTableParams, $stateParams) {
    var tokenCookie = $cookies.get('token');
    if (!tokenCookie) {
        localStorage.clear();
        $state.go('login');
    } 
    $rootScope.pageTitle = "Applicants Admission Information Details";  

    $scope.testclickApp_Photo = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].ApplicantPhoto != undefined && $scope.info[i].ApplicantPhoto != null &&
            $scope.info[i].ApplicantPhoto != "") {

            $("#imageID").attr('src', $scope.info[i].ApplicantPhoto);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickApp_Sign = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].ApplicantSignature != undefined && $scope.info[i].ApplicantSignature != null &&
            $scope.info[i].ApplicantSignature != "") {

            $("#imageID").attr('src', $scope.info[i].ApplicantSignature);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickAadhar = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].AadharDoc != undefined && $scope.info[i].AadharDoc != null &&
            $scope.info[i].AadharDoc != "") {

            $("#imageID").attr('src', $scope.info[i].AadharDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickDOB = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].DOBDoc != undefined && $scope.info[i].DOBDoc != null &&
            $scope.info[i].DOBDoc != "") {

            $("#imageID").attr('src', $scope.info[i].DOBDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickPC = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].PCDoc != undefined && $scope.info[i].PCDoc != null &&
            $scope.info[i].PCDoc != "") {

            $("#imageID").attr('src', $scope.info[i].PCDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickEWS = function (i, ApplicantAdmDetail) {
       // debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].EWSDoc != undefined && $scope.info[i].EWSDoc != null &&
            $scope.info[i].EWSDoc != "") {

            $("#imageID").attr('src', $scope.info[i].EWSDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickRes = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].ReservationCategoryDoc != undefined && $scope.info[i].ReservationCategoryDoc != null &&
            $scope.info[i].ReservationCategoryDoc != "") {

            $("#imageID").attr('src', $scope.info[i].ReservationCategoryDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickSoc = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].SocialCategoryDoc != undefined && $scope.info[i].SocialCategoryDoc != null &&
            $scope.info[i].SocialCategoryDoc != "") {

            $("#imageID").attr('src', $scope.info[i].SocialCategoryDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickEdu = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].AttachDocument != undefined && $scope.info[i].AttachDocument != null &&
            $scope.info[i].AttachDocument != "") {

            $("#imageID").attr('src', $scope.info[i].AttachDocument);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickAppDoc = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].ApplicationDoc != undefined && $scope.info[i].ApplicationDoc != null &&
            $scope.info[i].ApplicationDoc != "") {

            $("#imageID").attr('src', $scope.info[i].ApplicationDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickPass = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].PassbookDoc != undefined && $scope.info[i].PassbookDoc != null &&
            $scope.info[i].PassbookDoc != "") {

            $("#imageID").attr('src', $scope.info[i].PassbookDoc);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickRTGS = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].RTGSForm != undefined && $scope.info[i].RTGSForm != null &&
            $scope.info[i].RTGSForm != "") {

            $("#imageID").attr('src', $scope.info[i].RTGSForm);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }
    $scope.testclickAuth = function (i, ApplicantAdmDetail) {
        //debugger
        $scope.info = ApplicantAdmDetail;

        if ($scope.info[i].AuthorityLetter != undefined && $scope.info[i].AuthorityLetter != null &&
            $scope.info[i].AuthorityLetter != "") {

            $("#imageID").attr('src', $scope.info[i].AuthorityLetter);
        }
        else {

            $("#imageID").attr('src', "No File");
        }
    }

$scope.AutoFetchedPRN = $stateParams.PRN;

    $scope.GetLocalStorageValue = function () {

        if ($localStorage.ApplicantInfo.FlagFromApplicantInfo == true) {
            $localStorage.ApplicantInfo.FlagFromApplicantInfo = false;
            $scope.AdmissionInfo.UserName = $localStorage.ApplicantInfo.UserName;
           
        }
        else {
            $localStorage.ApplicantInfo = null;
            $scope.AdmissionInfo = null;
        }
    };

  

    $scope.RedirectToResult = function (info) {

        $localStorage.IncStudAdm = info;
        /* $state.go('ProvisionalResult');*/
      
        if ($localStorage.IncStudAdm.EvaluationId == 3) {
            $state.go('ProvisionalResult');
        }
        else if ($localStorage.IncStudAdm.EvaluationId == 2) {
            $state.go('DirectGradeProvisionalResult');
        }
        else if ($localStorage.IncStudAdm.EvaluationId == 1) {
            $state.go('InDirectGradeProvisionalResult');
        }
        else {
            alert('NO DATA FOUND');
            $state.go('ApplicantsAdmissionInformationDetails');
        }
    }
    
    $scope.tabName = "tab1";
    // code to switch you views based on tabs
    // sets bydefault true on div
    $scope.TabP = function () {
        $scope.PersonalList = false;
        $scope.Personal = true;
        $scope.Admission = false;
        $scope.Education = false;
        $scope.RequestStatus = false;
        $scope.PaperList = false;
        $scope.PreExamination = false;
        $scope.Cancel = false;
        $scope.CancelNoRecord = false;

       
    }
   
    $scope.TabAd = function () {
        $scope.PersonalList=true
        $scope.Personal = false;
        $scope.Admission = true;
        $scope.Education = false;
        $scope.RequestStatus = false;
        $scope.PaperList = false;
        $scope.PreExamination = false;
        $scope.Cancel = false;
        $scope.CancelNoRecord = false;


    }
    $scope.TabEdu = function () {
        $scope.PersonalList = true
        $scope.Personal = false;
        $scope.Admission = false;
        $scope.Education = true;
        $scope.RequestStatus = false;
        $scope.PaperList = false;
        $scope.PreExamination = false;
        $scope.Cancel = false;
        $scope.CancelNoRecord = false;


    }
    $scope.TabPaper = function () {
        $scope.PersonalList = true
        $scope.Personal = false;
        $scope.Admission = false;
        $scope.Education = false;
        $scope.RequestStatus = false;
        $scope.PaperList = true;
        $scope.PreExamination = false;
        $scope.Cancel = false;
        $scope.CancelNoRecord = false;


    }
    $scope.TabReq = function () {
        $scope.PersonalList = true
        $scope.Personal = false;
        $scope.Admission = false;
        $scope.Education = false;
        $scope.RequestStatus = true;
        $scope.PaperList = false;
        $scope.PreExamination = false;
        $scope.Cancel = false;
        $scope.CancelNoRecord = false;


    }

    $scope.TabPre = function () {
        $scope.PersonalList = true
        $scope.Personal = false;
        $scope.Admission = false;
        $scope.Education = false;
        $scope.RequestStatus = false;
        $scope.PaperList = false;
        $scope.PreExamination = true;
        $scope.Cancel = false;
        $scope.CancelNoRecord = false;


    }
    
    $scope.TabCancel = function () {
        $scope.PersonalList = true
        $scope.CancelArray = new Array();
        for (var i = 0; i < $scope.ApplicantAdmDetail.AdmissionList.length; i++) {
            if ($scope.ApplicantAdmDetail.AdmissionList[i].CancelChangeRequest == "Requested") {
                $scope.CancelArray.push($scope.ApplicantAdmDetail.AdmissionList[i]);
                $scope.Cancel = true;
                $scope.Personal = false;
                $scope.Admission = false;
                $scope.Education = false;
                $scope.RequestStatus = false;
                $scope.PaperList = false;
                $scope.PreExamination = false;
                $scope.CancelNoRecord = false;

            }
            else {
                $scope.CancelNoRecord = true;
                $scope.Personal = false;
                $scope.Admission = false;
                $scope.Education = false;
                $scope.RequestStatus = false;
                $scope.PaperList = false;
                $scope.PreExamination = false;
                $scope.Cancel = false;

            }

        }
        if ($scope.CancelArray.length <= 0) {
            $scope.Cancel = false;
            $scope.CancelNoRecord = true;
        }
       
      
    }


    $scope.AdmissionInfo = {};
    $scope.checkflag = false;

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

    $scope.expand_row_Admission = function (id) {

        let element = document.getElementById('expand_row' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col_Admission" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col_Admission" + id).innerHTML = "+"
            element.add("collapse")
        }
    }

    $scope.expand_row_Cancel = function (id) {

        let element = document.getElementById('expand_row_Cancel' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col_Cancel" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col_Cancel" + id).innerHTML = "+"
            element.add("collapse")
        }
    }

    $scope.expand_row_Request = function (id) {

        let element = document.getElementById('expand_row_request' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col_RequestStatus" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col_RequestStatus" + id).innerHTML = "+"
            element.add("collapse")
        }
    }

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    
    $scope.goBack = function () {
        $state.go("dashboarddetailsadmin");
    }
    
    $scope.SubmitRegistrationId = function () {

        if ($scope.AdmissionInfo.UserName == null || $scope.AdmissionInfo.UserName == undefined || $scope.AdmissionInfo.UserName == "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter Applicant UserName / PRN / Application Id")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
            $scope.IsTabVisible = false;
            $scope.Personal = false;
            $scope.Admission = false;
            $scope.Education = false;
            $scope.RequestStatus = false;
            $scope.PaperList = false;
            $scope.PreExamination = false;
            $scope.Cancel = false;
            $scope.CancelNoRecord = false;
            $scope.PersonalList = false;
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ApplicantsAdmissionInformationDetails/ApplicantsAdmissionInfoDetailsGetByUserName',
                data: $scope.AdmissionInfo,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        $scope.offSpinner();
                        alert(response.obj);
                        $scope.AdmissionInfo = {};
                    }
                    if (response.response_code == "201") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                       // alert(response.obj);
                        $scope.offSpinner();                                            
                        $scope.AdmissionInfo = {};
                    }
                    else {
                        $scope.offSpinner();
                        $scope.IsTabVisible = true;
                        $scope.Personal = false;
                        $scope.Admission = false;
                        $scope.Education = false;
                        $scope.RequestStatus = false;
                        $scope.PaperList = false;
                        $scope.PreExamination = false;
                        $scope.Cancel = false;
                        $scope.CancelNoRecord = false;
                        $scope.PersonalList = false;
                        
                        $scope.ApplicantAdmDetail = response.obj;
                        $scope.ApplicantInformationTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        })
                        console.log($scope.ApplicantAdmDetail);
                      
                        $scope.myPRN = $scope.ApplicantAdmDetail.PreExaminationList[0].PRN;
                        
                      
                    }

                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    if ($scope.AutoFetchedPRN != 0) {
        $scope.AdmissionInfo.UserName = $scope.AutoFetchedPRN;
        console.log($scope.AdmissionInfo.UserName);
        $scope.SubmitRegistrationId();
    }
    
    $scope.AdmissionRecieptList = function (data) {
       
        $localStorage.AppObject = {};
        $localStorage.AppObject.AppId = data.ApplicationId;
        $state.go('AdmissionFeesPaidReceiptList');
    }

    $scope.HideShow = function (ApplicationId) {
        
        // alert(Id + "====" + TransactionStatus);
        $localStorage.AppObject = {};
        $localStorage.AppObject.AppId = ApplicationId;
      
              
            $state.go('FeesPaidReceipt');
        
    }

    $scope.AdmApplicationFeesPaidGetForPaymentReceipt = function () {
        
        var data = new Object();
              
        if ($localStorage.AppObject == null || $localStorage.AppObject == undefined) {
            
        }
        else if ($localStorage.AppObject.AppId == null || $localStorage.AppObject.AppId == undefined) {
            
        }
        else {
            $http({
                method: 'POST',
                url: 'api/AdmApplicationFeesPaid/AdmApplicationFeesPaidGet',
                //data: data,
                data: { "Id": $localStorage.AppObject.AppId },
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        $scope.AdmApplicationFeesPaidGet = response.obj;
                        $scope.AdmApplicationFeesPaidGetTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj


                        });
                    }
                    //console.log(dataset);
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

                });
        }

    }

    $scope.ClickOnAccord = function (ind) {

        $('.show').removeClass('show');
        $('.bg-primary').removeClass('bg-primary');
        $("#divActive" + ind).addClass("bg-primary");
        $("#collapse" + ind).addClass("show");
    }
   
    //============Start -- Mohini's Code on 08-Apr-2022

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    var doc = new jsPDF();

    //Get All data for Print ExamForm
    $scope.DownloadExamForm = function (info) {
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/ApplicantsAdmissionInformationDetails/ExamFormPrintData',
            data: info,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                    $scope.offSpinner();

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                }
                else {
                    
                    $scope.ExamFormPrintTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.ExamFormPrintData = response.obj[0];
                  
                    $scope.ExamFormPrintDataPaperDetails(info);

                    setTimeout(function () {
                        $scope.PrintHTMLExamForm();
                        $scope.offSpinner();
                    }, 3000);
                   
                    
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

        //doc.fromHTML(`<html><head><title>${title}</title></head><body>` + document.getElementById(divId).innerHTML + `</body></html>`);
        //doc.save(info.ApplicationId + '_' + 'ExamForm.pdf');
    }

    //Get All data of Paper for Print in ExamForm
    $scope.ExamFormPrintDataPaperDetails = function (info) {

        $http({
            method: 'POST',
            url: 'api/ApplicantsAdmissionInformationDetails/ExamFormPrintDataPaperDetails',
            data: info,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');
                    /*   $scope.offSpinner();*/

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    //$scope.offSpinner();
                }
                else {

                    $scope.ExamFormPaperDataTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.ExamFormPaperData = response.obj;

                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

        //doc.fromHTML(`<html><head><title>${title}</title></head><body>` + document.getElementById(divId).innerHTML + `</body></html>`);
        //doc.save(info.ApplicationId + '_' + 'ExamForm.pdf');
    }

    //Generate Barcode Code
    $scope.GenerateFormNobarcode = function () {
        
        JsBarcode("#FormNobarcode", $scope.ExamFormPrintData.FormNo, {
            format: "code39",
            textAlign: "center",
            textPosition: "bottom",
            font: "cursive",
            fontOptions: "plain",
            fontSize: 40,
            textMargin: 15,
            displayValue: false,
            height: 20,
            width: 1
            //text: "Special"
        });
    }

    //Function for HTML Form in Using PDF
    $scope.PrintHTMLExamForm = function () {
        $scope.GenerateFormNobarcode();
        //var doc = new jsPDF();
        var sTable = document.getElementById('PrintBlockExamForm').innerHTML;
        datetime = new Date().toLocaleDateString();
       
        var style = "<style>";
        style = style + "table {width: 100%;font: 12px Calibri;}";
        style = style + "table, th, td {border: solid 1px black; border-collapse: collapse;";
        style = style + "padding: 2px 3px;}";
        style = style + "</style>";

        // CREATE A WINDOW OBJECT.
        var win = window.open('', 'height=700,width=700');

        win.document.write('<html><head>');
        //win.document.write('<title>Profile</title>');   // <title> FOR PDF HEADER.
        win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
        win.document.write('</head>');
        win.document.write('<body>');
        //win.document.write(datetime);
        win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
        win.document.write('</body></html>');
        win.document.close(); 	// CLOSE THE CURRENT WINDOW.

        //var doc = new jsPDF();
        //var specialElementHandlers = {
        //    '#editor': function (element, renderer) {
        //        return true;
        //    }
        //};

        //doc.fromHTML($('#PrintBlockExamForm').html(), 15, 15, {
        //    'width': 170,
        //    'elementHandlers': specialElementHandlers
        //});
        //doc.save('ExamForm.pdf');

        win.print();    // PRINT THE CONTENTS.
        
    };

    //============End -- Mohini's Code on 08-Apr-2022
    
});