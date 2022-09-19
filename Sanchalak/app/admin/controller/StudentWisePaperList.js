app.controller('StudentWisePaperListCtrl', function ($scope, $http, $filter, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Student Wise Paper List";
    $scope.resetStudentWisePaperList = function () {
        $scope.StudentWisePaperList = {};
    }
    $scope.StudentWiseTable = false;
    $scope.getStudentWisePaperList = function () {
        $scope.contentPresent = false;
        var Obj = {
            ProgrammeInstancePartTermId: $scope.StudentWisePaperList.ProgrammeInstancePartTermId,
            AcademicYearId: $scope.StudentWisePaperList.AcademicYearId,
            InstituteId: $scope.StudentWisePaperList.InstituteId,
            ProgrammeId: $scope.StudentWisePaperList.ProgrammeId,
            ProgrammePartId: $scope.StudentWisePaperList.ProgrammePartId,
            SpecialisationId: $scope.StudentWisePaperList.SpecialisationId,

        }

        $http({
            method: 'POST',
            url: 'api/StudentWisePaperList/StudentWisePaperListGet',
            data: Obj,
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
                        $scope.StudentWisePaperListTableParams = new NgTableParams({
                        }, {
                            dataset: null
                        });
                        $scope.exportDataFull = undefined;

                    }
                    else {

                        alert(response.obj);
                        //$scope.offSpinner();
                    }
                }
                else {
                    $scope.StudentWisePaperListTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });

                    //$scope.StudentWisePaperList = response.obj[0];
                    $scope.ExcelStudentWisePaperList = response.obj;
                    $scope.StudentWiseTable = true;

                }
                if (response.obj.length == 0) {
                    $scope.contentPresent = true;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.IncAcademicYearListGet = function () {

        $http({
            method: 'Post',
            url: 'api/StudentWisePaperList/AcademicYearGet',
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.AcademicList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getMstInstitute = function () {

        $http({
            method: 'Post',
            url: 'api/StudentWisePaperList/MstInstituteGetById',
            data: { Id: $cookies.get('InstituteId') },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger
                $scope.InstituteList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.ProgrammeGetbyInstId = function () {
        var InstId = { InstituteId: $scope.StudentWisePaperList.InstituteId }
        $http({
            method: 'Post',
            url: 'api/StudentWisePaperList/MstProgrammeGetByInstituteId',
            data: InstId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.ProgrammeList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.ProgPartInstListGet = function () {
        var ProgId = { ProgrammeId: $scope.StudentWisePaperList.ProgrammeId }
        //alert(ProgId);
        $http({
            method: 'Post',
            url: 'api/StudentWisePaperList/MstProgrammePartGetByProgrammeId',
            data: ProgId,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.ProgPartList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.BranchGet = function () {

        $http({
            method: 'Post',
            url: 'api/StudentWisePaperList/SpecialisationGetByProgrammeId',
            data: { ProgrammeId: $scope.StudentWisePaperList.ProgrammeId, ProgrammePartId: $scope.StudentWisePaperList.ProgrammePartId },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.BList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.PTsGet = function () {
        var PTSpe = {
            AcademicYearId: $scope.StudentWisePaperList.AcademicYearId,
            ProgrammeId: $scope.StudentWisePaperList.ProgrammeId,
            ProgrammePartId: $scope.StudentWisePaperList.ProgrammePartId,
            SpecialisationId: $scope.StudentWisePaperList.SpecialisationId,
        }
        $http({
            method: 'Post',
            url: 'api/StudentWisePaperList/ProgrammeInstancePartTermGetBySpecId',
            data: PTSpe,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.PTList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.exportStudentWisePaperList = function () {

        if ($scope.ExcelStudentWisePaperList == undefined) {

            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }
        //alert("Please wait, Excel is being prepared...Do not click again on Export to Excel button until Excel is download");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "StudentWisePaperList_" + DateWithoutDashed + time;

        var mystyleSecond = {
            headers: true,
            style: 'font-size:25px;font-weight:bold;',
            caption: {
                title: 'Student Wise Paper List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'PRN', title: 'PRN' },
                { columnid: 'FullName', title: 'FullName' },
                { columnid: 'EmailId', title: 'Email Id' },
                { columnid: 'MobileNo', title: 'Mobile No' },
                { columnid: 'Paper1', title: 'Paper-1' },
                { columnid: 'Paper2', title: 'Paper-2' },
                { columnid: 'Paper3', title: 'Paper-3' },
                { columnid: 'Paper4', title: 'Paper-4' },
                { columnid: 'Paper5', title: 'Paper-5' },
                { columnid: 'Paper6', title: 'Paper-6' },
                { columnid: 'Paper7', title: 'Paper-7' },
                { columnid: 'Paper8', title: 'Paper-8' },
                { columnid: 'Paper9', title: 'Paper-9' },
                { columnid: 'Paper10', title: 'Paper-10' },
                { columnid: 'Paper11', title: 'Paper-11' },
                { columnid: 'Paper12', title: 'Paper-12' },
                { columnid: 'Paper13', title: 'Paper-13' },
                { columnid: 'Paper14', title: 'Paper-14' },
                { columnid: 'Paper15', title: 'Paper-15' }

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyleSecond, $scope.ExcelStudentWisePaperList]);
    };





})