app.controller('RefundCaseByAccountCtrl', function ($scope, $http, $rootScope, Upload, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Refund Process by Account";
   
    $scope.RefundCaseAccount = {};
    $scope.RefundAmountObj = {};
   
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
            url: 'api/RefundCaseByAccount/MstFacultyGet',
            data: $scope.RefundCaseAccount,
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

    //Function for check that BOB or Other
    $scope.getBankStatus = function () {

        $scope.currentDate = new Date();
        $scope.currentDate.setHours(0, 0, 0, 0);

        if ($scope.RefundCaseAccount.Id == null || $scope.RefundCaseAccount.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else if ($scope.RefundCaseAccount.RefundBankStatus == null || $scope.RefundCaseAccount.RefundBankStatus == "" || $scope.RefundCaseAccount.RefundBankStatus === undefined) {
            alert("Please select Bank Status...!");
        }
        else if (($scope.RefundCaseAccount.FromDate == null) || ($scope.RefundCaseAccount.FromDate == undefined)) {
            alert("Please select From Date...!");
        }
        else if (($scope.RefundCaseAccount.ToDate == null) || ($scope.RefundCaseAccount.ToDate == undefined)) {
            alert("Please select To Date...!");
        }
        else if ($scope.RefundCaseAccount.FromDate > $scope.RefundCaseAccount.ToDate) {
            alert("To Date should be Greater than From Date...!");
        }
        else if ($scope.RefundCaseAccount.FromDate >= $scope.currentDate) {
            alert("You can select only previous Date...!");
        }
        else if ($scope.RefundCaseAccount.ToDate >= $scope.currentDate) {
            alert("You can select only previous Date...!");
        }
        else if ($scope.RefundCaseAccount.RefundBankStatus == 'BOB') {
            $scope.BOBFlag = true;
            $scope.RefCaseAccountApplicantsListBOB();

        }
        else if ($scope.RefundCaseAccount.RefundBankStatus == 'Other') {
            $scope.BOBFlag = false;
            $scope.RefCaseAccountApplicantsListOther();
        }

    };

    //Function for get Applicant list for Refund Process in BOB Account
    $scope.RefCaseAccountApplicantsListBOB = function () {

        $scope.RefundCaseBOBTableparam = new NgTableParams({

        },
            { dataset: []});
        $scope.checkDataforBOB = false;
        $scope.RefundCaseAccount.FacultyId = $scope.RefundCaseAccount.Id;

        if ($scope.RefundCaseAccount.Id == null || $scope.RefundCaseAccount.Id === undefined){
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseByAccount/RefCaseAccountApplicantsListBOB',
                data: $scope.RefundCaseAccount,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseBOBTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.offSpinner();
                    $scope.ExportFullDataforRefund = response.obj;
                    if (response.obj.length == 0 || response.obj == 'No Records Found.') {
                        $scope.checkDataforBOB = true;
                    }
                    $scope.checkDataforOther = true;

                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    //Function for get Applicant list for Refund Process in Other than BOB Account
    $scope.RefCaseAccountApplicantsListOther = function () {
        $scope.RefundCaseOtherTableparam = new NgTableParams({

        },
            { dataset: [] });
        $scope.checkDataforOther = false;
        //var FacultyId = { FacultyId: $scope.RefundCaseAccount.Id };
        $scope.RefundCaseAccount.FacultyId = $scope.RefundCaseAccount.Id;

        if ($scope.RefundCaseAccount.Id == null || $scope.RefundCaseAccount.Id === undefined) {
            alert("Please select Faculty...!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/RefundCaseByAccount/RefCaseAccountApplicantsListOther',
                data: $scope.RefundCaseAccount,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    // $scope.ApplicantByProgPartTermList = response.obj;
                    $scope.RefundCaseOtherTableparam = new NgTableParams({

                    },
                        { dataset: response.obj });
                    $scope.offSpinner();
                    $scope.ExportFullDataforRefund = response.obj;
                    if (response.obj.length == 0 || response.obj == 'No Records Found.') {
                        $scope.checkDataforOther = true;
                    }
                    $scope.checkDataforBOB = true;

                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    //Excel Students Admission Fees Paid Report
    $scope.ExportRefundDatatoExcelBOB = function () {

        for (var i in $scope.ExportFullDataforRefund) {

            if ($scope.ExportFullDataforRefund[i].ProcessedOnAccount == "" || $scope.ExportFullDataforRefund[i].ProcessedOnAccount == null) {
                $scope.ExportFullDataforRefund[i].ProcessedOnAccount = 'NA';
            }
        }

        alert("Please wait, Excel is being prepared...");

        //var acYear = $('#acYear option:selected').text();
        //$scope.AppFeeReport.AcademicYearCode = acYear;

        //var AcademicYearCode = $scope.AppFeeReport.AcademicYearCode;

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Refund_Report_for_Account" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Refund Case Report for Account(BOB to BOB) on ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'TransDate', title: 'Tran Date' },
                { columnid: 'IFSCCode', title: '11 DIGIT IFSC CODE' },
                { columnid: 'AccountNumber', title: 'Account Number' },
                { columnid: 'AccountName', title: 'Name Of The Beneficiary' },
                { columnid: 'Description', title: 'Description' },
                { columnid: 'Currency', title: 'Currency' },
                { columnid: 'DC', title: 'D/C' },
                { columnid: 'RefundAmountByAudit', title: 'Amount' },
                { columnid: 'IsBOBAccount', title: 'Is BOB Account' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                { columnid: 'ApplicationId', title: 'ApplicationId' },
                { columnid: 'FullName', title: 'Applicant Name' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Name' },
                { columnid: 'RequestType', title: 'RequestType' },
                { columnid: 'AccountRemark', title: 'AccountRemark' },
                { columnid: 'RefundedTansactionId', title: 'RefundedTansactionId' },
                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ExportFullDataforRefund]);
    };

    //Excel Students Admission Fees Paid Report
    $scope.ExportRefundDatatoExcelOther = function () {

        for (var i in $scope.ExportFullDataforRefund) {

            if ($scope.ExportFullDataforRefund[i].ProcessedOnAccount == "" || $scope.ExportFullDataforRefund[i].ProcessedOnAccount == null) {
                $scope.ExportFullDataforRefund[i].ProcessedOnAccount = 'NA';
            }
            if ($scope.ExportFullDataforRefund[i].PassbookDoc == "" || $scope.ExportFullDataforRefund[i].PassbookDoc == null) {
                $scope.ExportFullDataforRefund[i].PassbookDoc = 'NA';
            }
            if ($scope.ExportFullDataforRefund[i].RTGSForm == "" || $scope.ExportFullDataforRefund[i].RTGSForm == null) {
                $scope.ExportFullDataforRefund[i].RTGSForm = 'NA';
            }
        }

        alert("Please wait, Excel is being prepared...");

        //var acYear = $('#acYear option:selected').text();
        //$scope.AppFeeReport.AcademicYearCode = acYear;

        //var AcademicYearCode = $scope.AppFeeReport.AcademicYearCode;

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Refund_Report_for_Account" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Refund Case Report for Account(BOB to Other) on ' + DateAndTime,
            },
            caption: {
                title: 'Refund Case Report for Account(BOB to Other) on ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'DebitAccountNumber', title: '14 DIGIT DEBIT AC NO' },
                { columnid: 'RefundAmountByAudit', title: 'AMOUNT' },
                { columnid: 'IFSCCode', title: '11 DIGIT IFSC CODE' },
                { columnid: 'AccountNumber', title: 'BENEFICIARY ACCOUNT NO' },
                { columnid: 'AccountName', title: 'NAME OF THE BENEFICIARY' },
                { columnid: 'BankName', title: 'NAME & BRANCH OF THE BANK' },
                { columnid: 'PassbookDoc', title: 'View Passbook Doc' },
                { columnid: 'RTGSForm', title: 'View RTGS Doc' },
                { columnid: 'IsBOBAccount', title: 'Is BOB Account' },
                { columnid: 'ProcessedOnAccount', title: 'Account Approved On' },
                { columnid: 'ApplicationId', title: 'ApplicationId' },
                { columnid: 'FullName', title: 'Applicant Name' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstancePartTermName', title: 'Programme Name' },
                { columnid: 'RequestType', title: 'RequestType' },
                { columnid: 'AccountRemark', title: 'AccountRemark' },
                { columnid: 'RefundedTansactionId', title: 'RefundedTansactionId' },
                //{ columnid: 'RefundAmountByAcademic', title: 'Refund Amount By Academic' },
                //{ columnid: 'RefundAmountByAudit', title: 'Refund Amount By Audit' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ExportFullDataforRefund]);
    };

    $scope.cancelRefundCaseList = function () {
        $scope.RefundCaseAccount = {};
        $scope.showFormFlag = false;
    };

    /*Start - Import Code for Refund data in BOB */

    $scope.exceljson = {};

    //Read excel data to our database
    $scope.ReadRefundExcelFile = function () {
      
        var fileCheck = {};
        fileCheck = document.getElementById("ngexcelfile").value;
        var allowedExtensions = /(.xlsx|.xls)$/i;

        if (fileCheck == "" ||
            fileCheck === undefined) {
            document.getElementById("ErrorMsgUploadFile").innerHTML = "Must upload the file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            return false;
        }
        else if (!allowedExtensions.exec(fileCheck)) {

            document.getElementById("ErrorMsgUploadFile").innerHTML = "It only accepts .xlx and .xlsx file";
            document.getElementById("SuccessMsgUploadFile").innerHTML = "";
            fileCheck.value = '';

            return false;
        }

        else {
            document.getElementById("ErrorMsgUploadFile").innerHTML = "";
            /*Checks whether the file is a valid excel file*/
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
            var xlsxflag = false; /*Flag for checking whether excel is .xls format or .xlsx format*/

            if ($("#ngexcelfile").val().toLowerCase().indexOf(".xlsx") > 0) {
                xlsxflag = true;
            }
            var reader = new FileReader();
            reader.onload = function (e) {

                var data = {};
                data = e.target.result;

                if (xlsxflag) {
                    var workbook = XLSX.read(data, { type: 'binary' });
                }
                else {
                    var workbook = XLS.read(data, { type: 'binary' });
                }

                var sheet_name_list = workbook.SheetNames;
                var cnt = 0;
                sheet_name_list.forEach(function (y) { /*Iterate through all sheets*/

                    if (xlsxflag) {
                        $scope.exceljson = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    }
                    else {
                        $scope.exceljson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                    }

                    if ($scope.exceljson.length > 0) {
                       
                        $scope.data = [];
                        for (var i = 0; i < $scope.exceljson.length; i++) {
                            //$scope.RefundFinal = {};
                            //$scope.RefundFinal = { ApplicationId: 1, AccountRemark : 1};
                            //$scope.RefundFinal = { FacultyId: 1, AcademicYearId: 1, ProgrammeInstancePartTermId: 1, AdmissionCommitteeId: 1 };
                            //$scope.RefundFinal.FacultyId = $scope.CentralAdmission.FacultyId;
                            //$scope.RefundFinal.AcademicYearId = $scope.CentralAdmission.AcademicYearId;
                            //$scope.RefundFinal.ProgrammeInstancePartTermId = $scope.CentralAdmission.ProgrammeInstancePartTermId;
                            //$scope.RefundFinal.AdmissionCommitteeId = $scope.CentralAdmission.AdmissionCommitteeId;
                            var customer_info = {
                                "ApplicationId": $scope.exceljson[i].ApplicationId,
                                "AccountRemark": $scope.exceljson[i].AccountRemark,
                                "RefundedTansactionId": $scope.exceljson[i].RefundedTansactionId,
                                "RequestType": $scope.exceljson[i].RequestType
                            };
                            $scope.data.push(customer_info);
                            $scope.$apply();
                            //$scope.data.push(customer_info);
                            //// $scope.data.push($scope.PaperInt);
                            //$scope.$apply();
                            //console.log($scope.data);

                        }
                    }
                });
                $scope.save($scope.data);
            }
            if (xlsxflag) {
                reader.readAsArrayBuffer($("#ngexcelfile")[0].files[0]);
            }
            else {
                reader.readAsBinaryString($("#ngexcelfile")[0].files[0]);
            }



        }

    };

    //Save excel data to our database
    $scope.save = function (data) {
       
        //if ($scope.CentralAdmission.FacultyId === null || $scope.CentralAdmission.FacultyId === undefined ||
        //    $scope.CentralAdmission.AcademicYearId === null || $scope.CentralAdmission.AcademicYearId === undefined ||
        //    $scope.CentralAdmission.ProgrammeInstancePartTermId === null || $scope.CentralAdmission.ProgrammeInstancePartTermId === undefined ||
        //    $scope.CentralAdmission.AdmissionCommitteeId === null || $scope.CentralAdmission.AdmissionCommitteeId === undefined) {

        //    alert("please check all fields !!!");
        //    fileCheck = {};
        //}

        //else {

            var params = [];

        $scope.exceljson = data;
        for (var i = 0; i < $scope.exceljson.length; i++) {

            //$scope.ApplicationId = $scope.exceljson[i].ApplicationId;
            //$scope.AccountRemark = $scope.exceljson[i].AccountRemark;
  
            if ($scope.exceljson[i].ApplicationId === undefined || $scope.exceljson[i].ApplicationId === null ||
                $scope.exceljson[i].AccountRemark === undefined || $scope.exceljson[i].AccountRemark === null ||
                $scope.exceljson[i].RefundedTansactionId === undefined || $scope.exceljson[i].RefundedTansactionId === null ||
                $scope.exceljson[i].RequestType === undefined || $scope.exceljson[i].RequestType === null) {

                    //alert("Please Upload Proper Excel File");
                    $("#ngexcelfile").val("");
                    document.getElementById("ErrorMsgUploadFile").innerHTML = "Please Upload Proper Data in Excel File..!!";
                    document.getElementById("SuccessMsgUploadFile").innerHTML = "";

                    return false;

            }


            else
            {
                var customer_info = {
                    "ApplicationId": $scope.exceljson[i].ApplicationId,
                    "AccountRemark": $scope.exceljson[i].AccountRemark,
                    "RefundedTansactionId": $scope.exceljson[i].RefundedTansactionId,
                    "RequestType": $scope.exceljson[i].RequestType,
                };
                params.push(customer_info);

            }
        }

        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/RefundCaseByAccount/UpdateRefundDatafromExcel',
            data: params,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $scope.offSpinner();
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $scope.offSpinner();
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }


                else {
                    /* if (response.obj == 'null') {
                            $scope.msg = "Error : Something Wrong! Please Upload Your Excel In below format.";
                            alert("error");
                        }*/
                    $scope.offSpinner();
                    alert(response.obj);



                    //$scope.IsUploadVisible = false;
                    //$scope.IsUploadAcademicVisible = false;
                    //$scope.CentralAdmission = {};
                    //$scope.CentralAdmission.UploadExcel = null;
                    $("#ngexcelfile").val('');
                    //$scope.getMstCentralAdmission();


                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });


      //  }

    }

    /*End - Import Coode for Store data into Database */

});



