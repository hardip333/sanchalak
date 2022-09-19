app.controller('ProgDefStatisticsFacultyCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }

    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.ProgDefStatisticsFacultyTableParams = new NgTableParams({
    }, {
        dataset: []
    });


    $scope.ProgDefStatisticsFaculty = {}


    $scope.getFacultyById = function () {
        $http({
            method: 'POST',
            url: 'api/ProgDefStatisticsFaculty/MstFacultyGetbyId',
            data: $scope.ProgDefStatisticsFaculty,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Faculty = response.obj[0];
                $scope.getAcademicYear();
            })
            .error(function (res) {
                alert(res);
            });

    };

    $scope.getAcademicYear = function () {

        $http({
            method: 'POST',
            url: 'api/ProgDefStatisticsFaculty/AcademicYearGetForDropDown',
            data: $scope.ProgDefStatisticsFaculty,
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
    $scope.getProgDefStatisticsFaculty = function () {

        $scope.ProgDefStatisticsFacultyTableParams = new NgTableParams({
        }, {
            dataset: []
        });


        $http({
            method: 'POST',
            url: 'api/ProgDefStatisticsFaculty/ProgDefStatisticsFacultyGet',
            data: { FacultyId: $scope.Faculty.Id, AcademicYearId: $scope.ProgDefStatisticsFaculty.AcademicYearId },
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
                        //$scope.ProgDefStatisticsFacultyTableParams = new NgTableParams({
                        //}, {
                        //    dataset: null
                        //});
                        //$scope.exportDataFull = undefined;
                        ////$scope.searchCaseResultFull = undefined;
                        ////$scope.offSpinner();
                    }
                    else {

                        alert(response.obj);
                        //$scope.offSpinner();
                    }
                }
                else {
                    $scope.ProgDefStatisticsFacultyTableParams = new NgTableParams({
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
    $scope.exportDataProgStat = function () {

        //if ($scope.exportDataFull == undefined) {

        //    alert("Please select Programme Instance Part Term then click on Search");
        //    return false;
        //}
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Programme_Defination_Statistics" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Programme Defination Statistics ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'CompleteStatus', title: 'Complete Status' },
                { columnid: 'VerficationStatus', title: 'Verfication Status' },
                { columnid: 'LaunchStatus', title: 'Launch Status' },
                { columnid: 'CompleteStatusAssessment', title: 'Complete Status Assessment' },
                { columnid: 'VerficationStatusAssessment', title: 'Verfication Status Assessment' },
                { columnid: 'LaunchStatusAssessment', title: 'Launch Status Assessment' },
                

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exportDataFull]);
    };




});

