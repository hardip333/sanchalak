app.controller('StudentTransferRequestCtrl', function ($scope, $http, $rootScope, $filter, $state, $localStorage, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Student Transfer Request";

    $scope.cardTitle = "Student Transfer Request Operation";
   // $scope.StudTrans = {};
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    };

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
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
    };
  
    $scope.resetStuTransferReq = function () {
        $scope.showFormFlag = false;
        $scope.StudTrans = {};
    }

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetbyId',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
              
                $scope.Institute = response.obj[0];
              
                $scope.getMstStudentTransferRequestByInstituteId();
                //$scope.getMstStudentTransferRequestFullDataByInstituteId();
                $scope.getMstStudentTransferRequestByDestInstituteId();
                //$scope.getMstStudentTransferRequestFullDataByDestInstId();
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstStudentTransferRequestByInstituteId = function () {

        var InstId = {
            InstituteId: $scope.Institute.InstituteId
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/MstStudentTransferRequestGetByInstituteId',
            data: InstId,
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
                        //$scope.AdmFeeNotPaidList = response.obj;
                        $scope.StudentTransferReqTableParams = new NgTableParams({}, { dataset: response.obj });                    
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMstStudentTransferRequestByDestInstituteId = function () {
    
        var InstId = {
            InstituteId: $scope.Institute.InstituteId
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/MstStudentTransferRequestGetByDestInstituteId',
            data: InstId,
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
                        //$scope.AdmFeeNotPaidList = response.obj;
                        $scope.StudentTransReqDestTableParams = new NgTableParams({}, { dataset: response.obj });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMstStudentTransferRequestFullDataByInstituteId = function () {

        var InstId = {
            InstituteId: $scope.Institute.InstituteId
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/MstStudentTransferRequestGetFullDataByInstituteId',
            data: InstId,
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
                        $scope.StudentTransferReqByInstId = response.obj;
                        //$scope.AdmFeeNotPaidList = response.obj;
                        //$scope.StudentTransferReqFullDataTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.exportStudTransferListByInstitute();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMstStudentTransferRequestFullDataByDestInstId = function () {

        var InstId = {
            InstituteId: $scope.Institute.InstituteId
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/MstStudentTransferRequestGetFullDataByDestInstId',
            data: InstId,
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
                        //$scope.AdmFeeNotPaidList = response.obj;
                        //$scope.StudTransReqFullDestInstDataTableParams = new NgTableParams({}, { dataset: response.obj });
                        $scope.StudTransReqFullDestInstData = response.obj;
                        $scope.exportStudTransferListByDestInst();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.UpdateStudentTransferRequestForword = function (StudId,Status) {

        var StudentId = {
            Id: StudId,
            SourceInstituteStatus: Status            
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/MstStudentTransferRequestSourceInstituteEdit',
            data: StudentId,
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
                        $scope.getMstStudentTransferRequestByInstituteId();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }

    $scope.UpdateStudentTransferRequestReject = function () {
        if ($scope.StudTrans.SourceInstituteRemark == null || $scope.StudTrans.SourceInstituteRemark === undefined) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Enter Remark before Save...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            var StudentId = {
                Id: $scope.StudTrans.Id,
                SourceInstituteStatus: 'Reject',
                SourceInstituteRemark: $scope.StudTrans.SourceInstituteRemark
            };
            $http({
                method: 'POST',
                url: 'api/MstStudentTransferRequest/MstStudentTransferRequestSourceInstituteEdit',
                data: StudentId,
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
                            $scope.showFormFlag = false;
                            $scope.StudTrans = {};
                            $scope.getMstStudentTransferRequestByInstituteId();
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.UpdateStudentTransferRequestApprovedByDestInst = function () {
       
        if ( $scope.StudTrans.IsFeeCategoryChanged == null || $scope.StudTrans.IsFeeCategoryChanged === undefined || $scope.StudTrans.IsFeeCategoryChanged == false ) {
           $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Yes or No before Save...")
                    .ariaLabel('Alert Dialog Demo')
                   .ok('Okay!')
            );
            
        }
        else if ($scope.StudTrans.IsFeeCategoryChanged == "Yes" && ($scope.StudTrans.FeeCategoryPartTermMapId == null || $scope.StudTrans.FeeCategoryPartTermMapId === undefined || $scope.StudTrans.IsFeeCategoryChanged == false )) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Select Fee category before Save...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            if ($scope.StudTrans.IsFeeCategoryChanged == "Yes") { $scope.StudTrans.IsFeeCategoryChanged = true; }
            else { $scope.StudTrans.IsFeeCategoryChanged = false; }
            $scope.onSpinner();
            var StudentId = {
                Id: $scope.StudTrans.Id,
                DestinationInstituteStatus: 'Approve',
                StudentPRN: $scope.StudTrans.StudentPRN,
                IsFeeCategoryChanged: $scope.StudTrans.IsFeeCategoryChanged,
                ChangedFeeCategoryPartTermMapId: $scope.StudTrans.FeeCategoryPartTermMapId,
                InstancePartTermName: $scope.StudTrans.InstancePartTermName,
                SourceInstituteName: $scope.StudTrans.SourceInstituteName,
                DestinationInstituteName: $scope.StudTrans.DestinationInstituteName,
                MobileNo: $scope.StudTrans.MobileNo,
                EmailId: $scope.StudTrans.EmailId,
                LastName: $scope.StudTrans.LastName,
                FirstName: $scope.StudTrans.FirstName,
                MiddleName: $scope.StudTrans.MiddleName,
                InstituteId: $scope.Institute.InstituteId
            };
            $http({
                method: 'POST',
                url: 'api/MstStudentTransferRequest/MstStudentTransferRequestDestInstituteEdit',
                data: StudentId,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $state.go('login');
                    }
                    else
                        if (response.response_code != "200") {
                            alert(response.obj);
                            $scope.offSpinner();
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                           $scope.offSpinner();
                            alert(response.obj);
                            $scope.getMstStudentTransferRequestByDestInstituteId();
                            $scope.showFormFlagDestApp = false;
                            $scope.showFormFlagDestRej = false;
                            $scope.StudTrans.IsFeeCategoryChanged = false;
                            $scope.showFormFlagDest = false;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.UpdateStudentTransferRequestRejectByDestInst = function () {
        if ($scope.StudTrans.DestinationInstituteRemark == null || $scope.StudTrans.DestinationInstituteRemark === undefined) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Enter Remark before Save...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            var StudentId = {
                Id: $scope.StudTrans.Id,
                DestinationInstituteStatus: 'Reject',
                DestinationInstituteRemark: $scope.StudTrans.DestinationInstituteRemark
            };
            $http({
                method: 'POST',
                url: 'api/MstStudentTransferRequest/MstStudentTransferRequestDestInstituteEdit',
                data: StudentId,
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
                            $scope.showFormFlagDestRej = false;
                            $scope.StudTrans = {};
                            $scope.getMstStudentTransferRequestByDestInstituteId();
                            $scope.showFormFlagDest = false;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.getStudentProfileByPRN = function () {

        var PRN = {
            PRN: $localStorage.Student.PRNStudent
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/StudentProfileGetByPRN',
            data: PRN,
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
                        $scope.TransferStudentPro = response.obj[0];   
                        $scope.getMstStudentTransferEducationDetailsByPRN();
                        $scope.StudTrans.MobileNo = $scope.TransferStudentPro.MobileNo;
                        $scope.StudTrans.EmailId = $scope.TransferStudentPro.EmailId;   
                        $scope.StudTrans.LastName = $scope.TransferStudentPro.LastName;  
                        $scope.StudTrans.FirstName = $scope.TransferStudentPro.FirstName;  
                        $scope.StudTrans.MiddleName = $scope.TransferStudentPro.MiddleName;  

                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getMstStudentTransferEducationDetailsByPRN = function () {
        
        var PRN = {
            StudentPRN: $localStorage.Student.PRNStudent
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/MstStudentTransferRequestGetEducationDetailsByPRN',
            data: PRN,
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
                        $scope.EducationDetailsList = response.obj;
                       //$scope.EducationDetailsTableParams = new NgTableParams({}, { dataset: response.obj });
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getFeeCatPartTermMapByPartTermId = function () {

        var PartTermId = {
            ProgInstPartTermId: $localStorage.Student.ProgInstPartTermId 
        };
        $http({
            method: 'POST',
            url: 'api/MstStudentTransferRequest/MstStudentTransferReqFeeCatPartTermMapIdGet',
            data: PartTermId,
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
                        $scope.FeeCatList = response.obj;                       
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.DisplayData = function (StuPRN,ComeFrom) {
      
        $localStorage.Student = {};
        $localStorage.Student.PRNStudent = StuPRN;
        $localStorage.Student.FlagFrom = ComeFrom;
       
        $state.go('StudentTransferProfile');
       // $scope.getMstStudentTransferEducationDetailsByPRN(StuPRN);
    };

    $scope.modifyStudTransferData = function (data) {
            $scope.showFormFlag = true;          
            $scope.StudTrans = data;

    };

    $scope.CancelStuTransferReqDestInst = function () {
        $scope.showFormFlagDest = false;
        $scope.showFormFlagDestRej = false;
        $scope.showFormFlagDestApp = false;
        $scope.StudTrans = {};
    }

    $scope.RejectDestStudTransferData = function (data) {
        $scope.showFormFlagDest = true;
        $scope.showFormFlagDestRej = true;
        $scope.showFormFlagDestApp = false;
        $scope.StudTrans = data;

    };

    $scope.ApproveDestStudTransferData = function (data) {
        $scope.showFormFlagDest = true;
        $scope.showFormFlagDestApp = true;
        $scope.showFormFlagDestRej = false;
        $scope.StudTrans = data;
        $localStorage.Student.ProgInstPartTermId = $scope.StudTrans.ProgInstPartTermId;
        $localStorage.Student.PRNStudent = $scope.StudTrans.StudentPRN;
        $scope.getStudentProfileByPRN();
        $scope.getFeeCatPartTermMapByPartTermId();
    };

    $scope.exportStudTransferListByInstitute = function () {

        if ($scope.StudentTransferReqByInstId == undefined) {

            alert("Record Not Exists");
            return false;
        }
        var FacultyName = $scope.Institute.FacultyName;
        var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentTransferList_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '   Institute Name: ' + InstituteName + '<br>' +
                    '  Student Transfer List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'StudentPRN', title: 'Student PRN' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'SourceInstituteName', title: 'Source Institute Name' },
                { columnid: 'DestinationInstituteName', title: 'Destination Institute Name' },
                { columnid: 'EligibleDegreeName', title: 'Eligible Degree Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'ExaminationBodyName', title: 'Examination Body Name' },
                { columnid: 'Institute Attended', title: 'Institute Attended' },
                { columnid: 'CityName', title: 'City Name' },
                { columnid: 'ExamPassMonth', title: 'Exam Pass Month' },
                { columnid: 'ExamPassYear', title: 'Exam Pass Year' },
                { columnid: 'ExamCertificateNumber', title: 'Exam Certificate Number' },
                { columnid: 'MarkObtained', title: 'Mark Obtained' },
                { columnid: 'MarkOutof', title: 'Mark Out of' },
                { columnid: 'Grade', title: 'Grade' },
                { columnid: 'CGPA', title: 'CGPA' },
                { columnid: 'PercentageEquivalenceCGPA', title: 'Percentage Equivalence CGPA' },
                { columnid: 'Percentage', title: 'Percentage' },
                { columnid: 'ClassName', title: 'Class Name' },
                { columnid: 'IsFirstTrial', title: 'Is First Trial' },
                { columnid: 'IsLastQualifyingExam', title: 'Is Last Qualifying Exam' },
                { columnid: 'LanguageName', title: 'Language Name' },
                { columnid: 'ResultStatus', title: 'Result Status' },
                { columnid: 'OtherCity', title: 'Other City' },
                { columnid: 'SourceInstituteStatus', title: 'Status' },
                { columnid: 'SourceInstituteRemark', title: 'Remark' },
                { columnid: 'UserName', title: 'Request Processed By' },
                { columnid: 'SourceRequestProcessedOnDate', title: 'Request Processed On' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.StudentTransferReqByInstId]);


       // var FacultyName = $scope.Institute.FacultyName;
       // var InstituteName = $scope.Institute.InstituteName;
       // var LongDate = new Date($.now());
       // //alert(LongDate);

       // var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
       // //alert(ShortDate);

       // var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
       // //alert(DateWithoutDashed);

       // var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
       // // alert(time);

       // var dateAndTime = ShortDate + time;
       // var ExcelFileName = "StudentTransferList_" + ShortDate + time;
     

       // var uri = 'data:application/vnd.ms-excel;base64,'         
       //     , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}, Institute Name: {InstituteName}</h2><h2>Student Transfer List ({dateAndTime})</h2>{table}</table></body></html>'
       //     , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
       //     , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



       // var table = document.getElementById("StudTransferFullDataId");      
       // $('.ng-table-filters').remove();
       // var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || FacultyName, InstituteName: InstituteName || InstituteName, table: table.innerHTML };

       //// $('.ng-table-sort-header').after(filters);
       // var url = uri + base64(format(template, ctx));      
       // var a = document.createElement('a');
       // a.href = url;
       // a.download = ExcelFileName + '.xls';
       // a.click();
    };

    $scope.exportStudTransferListByDestInst = function () {

        //if ($scope.StudTransReqFullDestInstData == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        var FacultyName = $scope.Institute.FacultyName;
        var InstituteName = $scope.Institute.InstituteName;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentTransferReceiveList_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'FacultyName: ' + FacultyName + '<br>' +
                    '   Institute Name: ' + InstituteName + '<br>' +
                    '  Student Transfer Receive List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'StudentPRN', title: 'Student PRN' },
                { columnid: 'InstancePartTermName', title: 'Programme Instance Part Term' },
                { columnid: 'SourceInstituteName', title: 'Source Institute Name' },
                { columnid: 'DestinationInstituteName', title: 'Destination Institute Name' },
                { columnid: 'EligibleDegreeName', title: 'Eligible Degree Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'ExaminationBodyName', title: 'Examination Body Name' },
                { columnid: 'Institute Attended', title: 'Institute Attended' },
                { columnid: 'CityName', title: 'City Name' },
                { columnid: 'ExamPassMonth', title: 'Exam Pass Month' },
                { columnid: 'ExamPassYear', title: 'Exam Pass Year' },
                { columnid: 'ExamCertificateNumber', title: 'Exam Certificate Number' },
                { columnid: 'MarkObtained', title: 'Mark Obtained' },
                { columnid: 'MarkOutof', title: 'Mark Out of' },
                { columnid: 'Grade', title: 'Grade' },
                { columnid: 'CGPA', title: 'CGPA' },
                { columnid: 'PercentageEquivalenceCGPA', title: 'Percentage Equivalence CGPA' },
                { columnid: 'Percentage', title: 'Percentage' },
                { columnid: 'ClassName', title: 'Class Name' },
                { columnid: 'IsFirstTrial', title: 'Is First Trial' },
                { columnid: 'IsLastQualifyingExam', title: 'Is Last Qualifying Exam' },
                { columnid: 'LanguageName', title: 'Language Name' },
                { columnid: 'ResultStatus', title: 'Result Status' },
                { columnid: 'OtherCity', title: 'Other City' },
                { columnid: 'DestinationInstituteStatus', title: 'Status' },
                { columnid: 'DestinationInstituteRemark', title: 'Remark' },
                { columnid: 'UserName', title: 'Request Processed By' },
                { columnid: 'DestinationRequestProcessedOnDate', title: 'Request Processed On' },
                { columnid: 'OldFee', title: 'Old Fee' },
                { columnid: 'NewFee', title: 'New Fee' },
                { columnid: 'OldFeeTotalAmount', title: 'Old Fee Total Amount' },
                { columnid: 'NewFeeTotalAmount', title: 'New Fee Total Amount' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.StudTransReqFullDestInstData]);

        //var FacultyName = $scope.Institute.FacultyName;
        //var InstituteName = $scope.Institute.InstituteName;
        //var LongDate = new Date($.now());
        ////alert(LongDate);

        //var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        ////alert(ShortDate);

        //var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        ////alert(DateWithoutDashed);

        //var time = "_" + LongDate.getHours() + ":" + LongDate.getMinutes() + ":" + LongDate.getSeconds();
        //// alert(time);

        //var dateAndTime = ShortDate + time;
        //var ExcelFileName = "StudentTransferReceiveList_" + ShortDate + time;


        //var uri = 'data:application/vnd.ms-excel;base64,'
        //    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table><h2>Faculty Name: {FacultyName}, Institute Name: {InstituteName}</h2><h2>Student Transfer Receive List ({dateAndTime})</h2>{table}</table></body></html>'
        //    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        //    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }



        //var table = document.getElementById("StudTransFullDataDestId");
        //$('.ng-table-filters').remove();
        //var ctx = { worksheet: name || 'Worksheet', dateAndTime: dateAndTime || dateAndTime, FacultyName: FacultyName || FacultyName, InstituteName: InstituteName || InstituteName, table: table.innerHTML };

        //// $('.ng-table-sort-header').after(filters);
        //var url = uri + base64(format(template, ctx));
        //var a = document.createElement('a');
        //a.href = url;
        //a.download = ExcelFileName + '.xls';
        //a.click();
    };

    $scope.backToList = function () {       

        if ($localStorage.Student.FlagFrom == 'DestInstitute') {
            $state.go('StudentTransferByDestInst');
        }
        if ($localStorage.Student.FlagFrom == 'SourceInstitute') {
            $state.go('StudentTransferRequest');
        }


    };
});