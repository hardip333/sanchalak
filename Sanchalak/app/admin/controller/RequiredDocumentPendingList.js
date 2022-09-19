app.controller('RequiredDocumentPendingListCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Required Document Pending List";

    $scope.cardTitle = "Required Document Pending List";
    
    $scope.IsPendDocVisible = false;
    $scope.RequiredPendingDoc = {};

    $scope.cancelPendingRequiredDocumentList = function () {
        $scope.RequiredPendingDoc = {};
        $scope.IsPendDocVisible = false;
        $scope.IsExcelButton = false;
        $scope.IsLabelVisible = false;
        $scope.NoPendingRecLabel = false;
    }
    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/RequiredDocumentPendingList/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                // $scope.Faculty = response.obj[0]; // Archana's code
                $scope.Institute = response.obj[0];
                // $scope.Faculty = response.obj; // Krunal's code               
                $scope.getProgrammeList();
                         

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.IncAcademicYearListGet = function () {
        $scope.IsPendDocVisible = false;
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentPendingList/IncAcademicYearGet',
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
        $scope.RequiredPendingDoc = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.RequiredPendingDoc.AcademicYearId }
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentPendingList/ProgrammeListGetByInstituteAcademicId',
            data: $scope.RequiredPendingDoc,
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
        $scope.RequiredPendingDoc = { InstituteId: $scope.Institute.InstituteId, AcademicYearId: $scope.RequiredPendingDoc.AcademicYearId, ProgrammeId: $scope.RequiredPendingDoc.ProgrammeId }
       
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentPendingList/IncProgramInstancePartTermGetbyInsIdAcadIdProgId',
            data: $scope.RequiredPendingDoc,
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

  
    $scope.getPendingRequiredDocumentList = function () {
        if ($scope.RequiredPendingDoc.AcademicYearId == null || $scope.RequiredPendingDoc.AcademicYearId == undefined || $scope.RequiredPendingDoc.AcademicYearId == ""

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
        else if ($scope.RequiredPendingDoc.ProgrammeId == null || $scope.RequiredPendingDoc.ProgrammeId == undefined || $scope.RequiredPendingDoc.ProgrammeId == ""

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
        else if ($scope.RequiredPendingDoc.ProgrammeInstancePartTermId == null || $scope.RequiredPendingDoc.ProgrammeInstancePartTermId == undefined || $scope.RequiredPendingDoc.ProgrammeInstancePartTermId == ""

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
            url: 'api/RequiredDocumentPendingList/RequiredPendingDocumentGetByIAPP',
            data: $scope.RequiredPendingDoc,
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
                    $scope.RequiredPendingDocument = response.obj;
                    for (let i = 0; i < $scope.RequiredPendingDocument.length; i++) {

                        $scope.ProgramLbl = {};
                        $scope.ProgramLbl.ProgrammeName = $scope.RequiredPendingDocument[i].ProgrammeName;
                        $scope.ProgramLbl.InstancePartTermName = $scope.RequiredPendingDocument[i].InstancePartTermName;
                    }
                    $scope.IsLabelVisible = true;
                    $scope.NoPendingRecLabel = false;
                    $scope.IsPendDocVisible = true;
                    $scope.RequiredPendingDocumentTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                   
                  
                    $scope.getPendingRequiredDocumentListForExcel();
                    
                   
                    
                  
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

        }
    };  

    $scope.getPendingRequiredDocumentListForExcel = function () {
      
       
        $http({
            method: 'POST',
            url: 'api/RequiredDocumentPendingList/RequiredPendingDocumentForExcel',
            data: $scope.RequiredPendingDoc,
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
                   
                    $scope.ReportData123 = response.obj;




                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };  

    $scope.displayPendingDocument = function (data) {
        
        $scope.obj = {};
        $scope.obj.ProgrammeId = $scope.RequiredPendingDoc.ProgrammeId;
        $scope.obj.ApplicationFormNo = data.ApplicationFormNo;


        $http({
            method: 'POST',
            url: 'api/RequiredDocumentPendingList/RequiredPendingDocumentAPP',
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
                        $scope.NoPendingDocRecLabel = true;
                        $scope.IsPendingDocumentVisible = false;
                    }
                }



                else {
                    $scope.NoPendingDocRecLabel = false;
                    $scope.IsPendingDocumentVisible = true;   
                    $scope.ReqPendingDocData = response.obj;





                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };  

    $scope.exportPendingDocumentData = function () {
       
        $scope.IsPendDocVisible = true;
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "RequiredPendingDocumentDetails_" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Required Pending Document Details | Date and Time: ' + DateAndTime,
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
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ReportData123]);
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