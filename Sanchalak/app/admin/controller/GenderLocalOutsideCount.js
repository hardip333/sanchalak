app.controller('GenderLocalOutsideCountCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Total Gender By Local and Outside Vadodara From Academic Year and Faculty  ";

    $scope.GenderLocalOutsideCount = {}

    /*Reset Academic Year Level*/
    $scope.resetGenderLocalOutsideCount = function () {
        $scope.GenderLocalOutsideCount = {};
    };

    /*Get Application Payment Report*/
    $scope.getGenderLocalOutsideCount = function (FacultyId, AcademicYearId) {
        //alert($scope.AdmissionFeesReportByAcademic.FacultyId);
        //alert($scope.AdmissionFeesReportByAcademic.AcademicYearId);

        $http({
            method: 'POST',
            url: 'api/GenderLocalOutsideCount/GenderLocalOutsideCountGet',
            data: { FacultyId: $scope.GenderLocalOutsideCount.FacultyId, AcademicYearId: $scope.GenderLocalOutsideCount.AcademicYearId },
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

                    $scope.GenderLocalOutsideCountTableParams = new NgTableParams({
                        page: 1,
                        count: 10
                        //count:response.obj.length

                    }, {
                        dataset: response.obj

                    });
                    $scope.exporttoexcel = response.obj;
                    console.log($scope.exporttoexcel);
                }
                //alert($scope.exporttoexcel[0].InstancePartTermName);
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Academic Year*/



    $scope.getFacultyById = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/GenderLocalOutsideCount/FacultyGetById',
            data: { Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                // $scope.Faculty = response.obj[0]; // Archana madam's code
                $scope.FacultyList = response.obj;

                // $scope.Faculty = response.obj; // Krunal's code    
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicYear = function () {
        $http({
            method: 'POST',
            url: 'api/GenderLocalOutsideCount/AcademicYearGetForDropDown',
            data: $scope.GenderLocalOutsideCount,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AYList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.exportData = function () {

        if ($scope.exporttoexcel == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "GenderLocalOutsideCount_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Gender Local Outside Count By Academic| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'Year', title: 'Year' },
                { columnid: 'LocalVadodaraMale', title: 'Local Vadodara Male' },
                { columnid: 'OutSideVadodaraMale', title: 'OutSide Vadodara Male' },
                { columnid: 'LocalGujaratMale', title: 'Local Gujarat Male' },
                { columnid: 'OutSideGujaratMale', title: 'OutSide Gujarat Male' },
                { columnid: 'LocalVadodaraFemale', title: 'Local Vadodara Female' },
                { columnid: 'OutSideVadodaraFemale', title: 'OutSide Vadodara Female' },
                { columnid: 'LocalGujaratFemale', title: 'Local Gujarat Female' },
                { columnid: 'OutSideGujaratFemale', title: 'OutSide Gujarat Female' },
                { columnid: 'LocalVadodaraTransGender', title: 'Local Vadodara TransGender' },
                { columnid: 'OutSideVadodaraTransGender', title: 'OutSide Vadodara TransGender' },
                { columnid: 'LocalGujaratTransgender', title: 'Local Gujarat Transgender' },
                { columnid: 'OutSideGujaratTransgender', title: 'OutSide Gujarat Transgender' },
                { columnid: 'TotalStudents', title: 'Total Students' },
                
                
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exporttoexcel]);
    };

});

