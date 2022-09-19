app.controller('ProgDefStatisticsAcademicsCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }


    $scope.ProgDefStatisticsAcademics = {}

    
    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/ProgDefStatisticsAcademics/FacultyGet',
            data: $scope.ProgDefStatisticsAcademics,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicYear = function () {

        $http({
            method: 'POST',
            url: 'api/ProgDefStatisticsAcademics/AcademicYearGetForDropDown',
            data: $scope.ProgDefStatisticsAcademics,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    //Get Students Admission Fees Paid Report
    $scope.getProgDefStatisticsAcademics = function () {
        
        $http({
            method: 'POST',
            url: 'api/ProgDefStatisticsAcademics/ProgDefStatisticsAcademicsGet',
            data: { FacultyId: $scope.ProgDefStatisticsAcademics.FacultyId, AcademicYearId: $scope.ProgDefStatisticsAcademics.AcademicYearId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                //alert("Please wait, Data is processing...");
                //$scope.onSpinner();

                if (response.response_code != "200") {
                    //debugger;
                    if (response.obj == "The source contains no DataRows.") {

                        alert("No Record Found!");
                        $scope.ProgDefStatisticsAcademicsTableParams = new NgTableParams({
                        }, {
                            dataset: null
                        });
                        $scope.exportDataFull = undefined;
                        //$scope.searchCaseResultFull = undefined;
                        //$scope.offSpinner();
                    }
                    else {

                        alert(response.obj);
                        //$scope.offSpinner();
                    }
                }
                else {
                    $scope.ProgDefStatisticsAcademicsTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.exportDataFull = response.obj;
                    //$scope.ApplicationListSearchFull();
                }
                /*console.log("=====");
                console.log($scope.ApplicationListTableParams);*/
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    //Excel Students Admission Fees Paid Report
    $scope.exportData = function () {

        if ($scope.exportDataFull == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentAdmissionFeesPaidReport_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Student Admission Fees Paid Report | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'OrderId', title: 'Order Id' },
                { columnid: 'FullName', title: 'Name' },
                { columnid: 'EmailId', title: 'Email ID' },
                { columnid: 'MobileNo', title: 'Mobile No.' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'GroupName', title: 'Group Name' },
                { columnid: 'FeeCategoryName', title: 'Fee Category Name' },
                { columnid: 'IsAdmissionFeePaid', title: 'Is Admission Fee Paid' },
                { columnid: 'IsInstalmentSelected', title: 'Is Instalment Selected' },
                { columnid: 'IsVerificationEmail', title: 'Is Verification Email' },
                { columnid: 'IsVerificationSms', title: 'Is Verification Sms' },
                { columnid: 'IsVerificationEmailOns', title: 'Is Verification Email On' },
                { columnid: 'IsVerificationSmsOns', title: 'Is Verification Sms On' },
                { columnid: 'TotalAmount', title: 'Total Amount' },
                { columnid: 'AmountPaid', title: 'Amount Paid' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
    };

    


});

