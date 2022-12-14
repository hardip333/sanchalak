app.controller('AdmissionFeesReportByAcademicCtrl', function ($scope, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Admission Fees Report for Academics  ";

    $scope.AdmissionFeesReportFaculty = {}

    /*Reset Academic Year Level*/
    $scope.resetAdmissionFeesReportByAcademic = function () {
        $scope.AdmissionFeesReportByAcademic = {};
    };

    /*Get Application Payment Report*/
    $scope.getAdmissionFeesReportByAcademic = function (FacultyId, AcademicYearId) {
        //alert($scope.AdmissionFeesReportByAcademic.FacultyId);
        //alert($scope.AdmissionFeesReportByAcademic.AcademicYearId);

        $http({
            method: 'POST',
            url: 'api/AdmissionFeesReportByAcademic/AdmissionFeesReportByAcademicGet',
            data: { FacultyId: $scope.AdmissionFeesReportByAcademic.FacultyId, AcademicYearId: $scope.AdmissionFeesReportByAcademic.AcademicYearId },
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

                    $scope.AdmissionFeesReportByAcademicTableParams = new NgTableParams({
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



    $scope.getFaculty = function () {
        $http({
            method: 'POST',
            url: 'api/AdmissionFeesReportByAcademic/MstFacultyGetForDropDown',
            data: $scope.AdmissionFeesReportByAcademic,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getAcademicYear = function () {
        $http({
            method: 'POST',
            url: 'api/AdmissionFeesReportByAcademic/AcademicYearGetForDropDown',
            data: $scope.AdmissionFeesReportByAcademic,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AYList = response.obj;
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.displayAdmissionFeesReportByAcademic = function (ProgrammeInstancePartTermId) {

        //debugger;
        if (ProgrammeInstancePartTermId == undefined) {
            ProgrammeInstancePartTermId = $localStorage.SelectedInstancePartTermId;
        }
        else {
            $localStorage.SelectedInstancePartTermId = $scope.AdmissionFeesReportByAcademic.ProgrammeInstancePartTermId;
        }
        //alert("Local" + $localStorage.SelectedInstancePartTermId);
        $scope.AdmissionFeesReportByAcademic = ProgrammeInstancePartTermId;
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
        var ExcelFileName = "AdmissionFeesReportByAcademic_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Admission Fees Report By Academic| Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'AcademicYearCode', title: 'AcademicYearCode' },
                { columnid: 'InstancePartTermName', title: 'InstancePartTermName' },
                { columnid: 'AdmissionFeeStatus', title: 'AdmissionFeeStatus' },
                { columnid: 'PublishedStatus', title: 'PublishedStatus' },
                { columnid: 'FeeTypeName', title: 'FeeTypeName' },
                { columnid: 'FacultyName', title: 'FacultyName' },
                { columnid: 'ProgrammeName', title: 'ProgrammeName' },
                { columnid: 'BranchName', title: 'BranchName' },
                { columnid: 'FeeCategoryName', title: 'Fee Category Name' },
                { columnid: 'TotalAmount', title: 'TotalAmount' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.exporttoexcel]);
    };

});

