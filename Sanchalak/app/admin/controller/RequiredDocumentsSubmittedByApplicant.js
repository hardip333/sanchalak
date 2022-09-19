app.controller('RequiredDocumentsSubmittedByApplicantCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Required Documents Submitted By Applicant";

    $scope.cardTitle = "Required Documents Submitted By Applicant";

    $scope.IsPendDocVisible = false;
    $scope.RequiredDocSubmitted = {};

    $scope.cancelPendingRequiredDocumentList = function () {
        $scope.RequiredDocSubmitted = {};
        $scope.IsPendDocVisible = false;
        $scope.IsExcelButton = false;
        $scope.NoPendingRecLabel = false;
        $scope.IsLabelVisible = false;
    }

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/RequiredDocumentsSubmittedByApplicant/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
               
                $scope.Institute = response.obj[0];       

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.IncAcademicYearListGet = function () {
        $scope.AcademicYearList = {};
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentsSubmittedByApplicant/IncAcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.AcademicYearList = response.obj;

            })
            .error(function (res) {

            });
    };

    $scope.getProgrammeListByInstIdAcadId = function () {
       
        $scope.ProgrammeList = {};
        $scope.RequiredDocSubmitted = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.RequiredDocSubmitted.AcademicYearId }
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentsSubmittedByApplicant/ProgrammeListGetByInstituteAcademicId',
            data: $scope.RequiredDocSubmitted,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeList = response.obj;
            })
            .error(function (res) {
                alert(res);
                $scope.ProgrammeList = {};
            });
    };

    $scope.getInstanceNameList = function () {
        $scope.InstanceNameList = {};
        $scope.RequiredDocSubmitted = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.RequiredDocSubmitted.AcademicYearId, ProgrammeId: $scope.RequiredDocSubmitted.ProgrammeId }

        $http({
            method: 'POST',
            url: 'api/RequiredDocumentsSubmittedByApplicant/IncProgramInstancePartTermGetbyInsIdAcadIdProgId',
            data: $scope.RequiredDocSubmitted,
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

                    $scope.InstanceNameList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getRequiredSubmittedDocumentList = function () {

        if ($scope.RequiredDocSubmitted.AcademicYearId == null || $scope.RequiredDocSubmitted.AcademicYearId == undefined || $scope.RequiredDocSubmitted.AcademicYearId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Academic Year DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.RequiredDocSubmitted.ProgrammeId == null || $scope.RequiredDocSubmitted.ProgrammeId == undefined || $scope.RequiredDocSubmitted.ProgrammeId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Programme DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.RequiredDocSubmitted.ProgrammeInstancePartTermId == null || $scope.RequiredDocSubmitted.ProgrammeInstancePartTermId == undefined || $scope.RequiredDocSubmitted.ProgrammeInstancePartTermId == ""

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Programme Instance Part Term DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
        $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentsSubmittedByApplicant/RequiredSubmittedDocumentsGetByFAPPTd',
            data: $scope.RequiredDocSubmitted,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.NoPendingRecLabel = true;
                    $scope.IsLabelVisible = false;
                    $scope.IsPendDocVisible = false;
                    $scope.offSpinner();
                }

                else {
                    $scope.offSpinner();
                    $scope.RequiredSubmittedDocument = response.obj;
                    for (let i = 0; i < $scope.RequiredSubmittedDocument.length; i++) {
                        $scope.ProgramLbl = {};
                        $scope.ProgramLbl.ProgrammeName = $scope.RequiredSubmittedDocument[i].ProgrammeName;
                        $scope.ProgramLbl.InstancePartTermName = $scope.RequiredSubmittedDocument[i].InstancePartTermName;

                    }
                    $scope.IsLabelVisible = true;
                    $scope.NoPendingDocRecLabel = false;
                    $scope.IsPendDocVisible = true;
                    $scope.RequiredSubmittedDocumentTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                   

                    $scope.getSubmittedRequiredDocumentListForExcel();




                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
        }
    };

    $scope.getSubmittedRequiredDocumentListForExcel = function () {
    

        $http({
            method: 'POST',
            url: 'api/RequiredDocumentsSubmittedByApplicant/RequiredSubmittedDocumentListExcelGetByAIPP',
            data: $scope.RequiredDocSubmitted,
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

                    $scope.ReportSubmittedDocument1 = response.obj;




                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.displaySubmittedDocument = function (data) {

        $scope.obj = {};
        $scope.obj.ProgrammeId = $scope.RequiredDocSubmitted.ProgrammeId;
        $scope.obj.ApplicationFormNo = data.ApplicationFormNo;


        $http({
            method: 'POST',
            url: 'api/RequiredDocumentsSubmittedByApplicant/RequiredSubmittedDocumentGetByAP',
            data: $scope.obj,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.obj == "No Record Found") {
                        $scope.NoSubmittedDocRecLabel = true;
                        $scope.IsPendingDocumentVisible = false;
                    }
                }



                else {
                    $scope.NoSubmittedDocRecLabel = false;
                    $scope.IsPendingDocumentVisible = true;
                    $scope.ReqSubmittedDocData = response.obj;





                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.exportSubmittedDocumentData = function () {
       
        $scope.IsPendDocVisible = true;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "RequiredSubmittedDocumentDetails_" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Required Submitted Document Details | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'ApplicationFormNum', title: 'Application Form Number' },
                { columnid: 'UserName', title: 'Applicant UserName' },
                { columnid: 'NameAsPerMarksheet', title: 'Applicant Name' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'NameOfTheDocument', title: 'Name Of The Document' },


            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ReportSubmittedDocument1]);
    };





    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


});