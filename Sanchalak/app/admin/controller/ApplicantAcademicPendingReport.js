app.controller('ApplicantAcademicPendingReportCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Applicant Academic Verification Pending Report";
    $scope.AVPending = {};

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }
    $scope.FacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/ApplicantAcademicVerificationPendingReport/FacultyGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');
                }
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    if (response.response_code == "201") {
                        $scope.FacultyList = {};

                    }
                }
                else {
                    $scope.FacultyList = response.obj;
                    
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };
    $scope.IncAcademicYearListGet = function () {
     
        $http({
            method: 'POST',
            url: 'api/ApplicantAcademicVerificationPendingReport/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.AcademicYearList = response.obj;
            })
            .error(function (res) {

            });
    };

    $scope.getApplicantAcademicVerificationPendingReport = function () {
      
        if ($scope.AVPending.AcademicYearId == null || $scope.AVPending.AcademicYearId === undefined || $scope.AVPending.AcademicYearId == "" ||
            $scope.AVPending.FacultyId == null || $scope.AVPending.FacultyId === undefined || $scope.AVPending.FacultyId == "" 

        )
        {
            
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select The Above DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.onSpinner();
        $http({
            method: 'POST',
            url: 'api/ApplicantAcademicVerificationPendingReport/ApplicantAcademicVerificationPendingListGetbyFIdAId',
            data: $scope.AVPending,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {

                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.offSpinner();
                    $scope.NoRecLabel = true;
                    $scope.IsTableVisible = false;
                    $scope.IsExcelButton = false;

                }

                else {
                 
                    $scope.offSpinner();
                    $scope.ApplicantAcademicVerification = response.obj;

                    $scope.IsTableVisible = true;
                    $scope.IsExcelButton = true;
                    $scope.NoRecLabel = false;
                    $scope.ApplicantAcademicVerificationTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    })
                   
                }

            })

            .error(function (res) {


                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);

            });
        }
    };

    $scope.cancelApplicantAcademicVerificationPendingReport = function () {
        $scope.AVPending = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
        $scope.NoRecLabel = false;
    };

    $scope.exportData = function () {
       
        for (var i in $scope.FacultyList) {
            if ($scope.FacultyList[i].Id == $scope.AVPending.FacultyId) {
                $scope.AVPending.FacultyName = $scope.FacultyList[i].FacultyName;

            }

        }
       
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ApplicantAcademicVerificationPendingList" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'ApplicantAcademicVerificationPendingList|Faculty Name:' + $scope.AVPending.FacultyName +':' +'Date and Time: ' +'<br>'+ DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'Id', title: 'ApplicationFormNo' },
                { columnid: 'NameAsPerMarksheet', title: 'Applicant Name' },
                { columnid: 'InstancePartTermName', title: 'InstancePartTermName' },
                { columnid: 'EligibilityByAcademics', title: 'Academics Eligibility Status' },
                { columnid: 'AdminRemarkByAcademics', title: 'Academic Remark' },
                { columnid: 'ApprovedOnAcademics', title: 'Approved Academic Date' },





            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ApplicantAcademicVerification]);
    };
   
   
});