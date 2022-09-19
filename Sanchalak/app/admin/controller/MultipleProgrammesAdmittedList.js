app.controller('MultipleProgrammesAdmittedListCtrl', function ($scope, $http, $rootScope, Upload, $filter, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Multiple Programmes Admitted List";
   
    $scope.MultipleAdmitted = {};
    //$scope.ExamEvent = {};

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    //Academic Year List Get
    $scope.GetAcademicYearList = function () {

        $http({
            method: 'POST',
            url: 'api/MultipleProgrammesAdmittedList/GetAcademicYearList',
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
                        $scope.AcademicList = {};

                    }
                }
                else {
                    $scope.AcademicList = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    //Function for Get Multiple Programmes Admitted Students List
    $scope.GetMultipleProgrammesAdmittedList = function () {

        $scope.checkDataExists = false;

        if ($scope.MultipleAdmitted.AcademicYearId == null || $scope.MultipleAdmitted.AcademicYearId == "" || $scope.MultipleAdmitted.AcademicYearId === undefined) {
            alert("Please select Academic Year...!");
            $scope.checkDataExists = true;
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/MultipleProgrammesAdmittedList/GetMultipleProgrammesAdmittedList',
                data: $scope.MultipleAdmitted,
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
                            $scope.AcademicList = {};
                            $scope.checkDataExists = true;
                        }
                    }
                    else {
                        $scope.AdmittedListTableparam = new NgTableParams({

                        },
                            { dataset: response.obj });
                        $scope.MultipleAdmittedList = response.obj;
                        $scope.offSpinner();
                        if (response.obj.length == 0) {
                            $scope.checkDataExists = true;
                        }
                    }
                })
                .error(function (res) {
                    alert(res);
                });
        }
    };

    $scope.cancelAdmittedList = function () {
        $scope.MultipleAdmitted = {};
        $scope.showFormFlag = false;
    };

    //Excel Code for Schedule Time-Table
    $scope.ExportMultipleAdmittedDatatoExcel = function () {

        //for (var i in $scope.MultipleAdmittedList) {

        //    if ($scope.MultipleAdmittedList[i].IsPRNGenerated == "" || $scope.MultipleAdmittedList[i].IsPRNGenerated == null) {
        //        $scope.MultipleAdmittedList[i].IsPRNGenerated = 'false';
        //    }
        //}


        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Multiple_Programmes_Admitted_Applicant_Data " + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            //caption: {
            //    title: 'Time-Table Scheduled Data on ' + DateAndTime,
            //},
            columns: [
                { columnid: 'IndexId', title: 'Sr No.' },
                { columnid: 'ApplicationId', title: 'ApplicationId' },
                { columnid: 'StudentName', title: 'Student Name' },
                { columnid: 'MobileNo', title: 'MobileNo' },
                { columnid: 'EmailId', title: 'EmailId' },
                { columnid: 'AdmittedFaculty', title: 'Admitted Faculty' },
                { columnid: 'AdmittedCourse', title: 'Admitted Course' },
                { columnid: 'EligibleApplicationid', title: 'Eligible Applicationid' },
                { columnid: 'EligibleCourseFaculty', title: 'Eligible Course Faculty' },
                { columnid: 'EligibleCourse', title: 'Eligible Course' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.MultipleAdmittedList]);
    };

});



