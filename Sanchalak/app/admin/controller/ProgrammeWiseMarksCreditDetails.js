app.controller('ProgrammeWiseMarksCreditDetailsCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Programme Wise Marks Credit Details Report";
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    /*CancelCenterWiseBlockReport*/
    $scope.resetProgrammeWiseMarksCreditDetails = function () {
        $scope.ProgMarksCredit = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
    };

    $scope.MstFacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeWiseMarksCreditDetails/MstFacultyGetForDropDown',
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
                    $scope.FacultyList  = response.obj;

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.MstInstituteGetByFacultyId = function () {

        $scope.InstituteList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeWiseMarksCreditDetails/InstituteGetByFacultyId',
            data: $scope.ProgMarksCredit,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.InstituteList = {};
                }
                else {
                    $scope.InstituteList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

    $scope.IncAcademicYearGet = function () {

        $scope.AcademicYearList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeWiseMarksCreditDetails/IncAcademicYearGet',
            data: $scope.ProgMarksCredit,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;

                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    $scope.AcademicYearList = {};
                }
                else {
                    $scope.AcademicYearList = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });

    };

 
    $scope.getProgrammeWiseMarksCreditDetails = function () {
        if ($scope.ProgMarksCredit.FacultyId === null || $scope.ProgMarksCredit.FacultyId === undefined ||
            $scope.ProgMarksCredit.InstituteId === null || $scope.ProgMarksCredit.InstituteId === undefined ||
            $scope.ProgMarksCredit.AcademicYearId === null || $scope.ProgMarksCredit.AcademicYearId === undefined) {
            alert("please check all fields !!!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ProgrammeWiseMarksCreditDetails/ProgrammeWiseMarksCreditDetailsById',
                data: $scope.ProgMarksCredit,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                                              
                        $scope.offSpinner();
                        alert(response.obj);
                        $scope.NoRecLabel = true;
                        $scope.IsTableVisible = false;
                        $scope.IsExcelButton = false;

                    }
                    else {
                        $scope.offSpinner();
                        $scope.NoRecLabel = false;
                        $scope.IsTableVisible = true;
                        $scope.IsExcelButton = true;
                        $scope.ProgrammeWiseMarksCreditDetailsTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                        $scope.ProgrammeWiseMarksCreditDetailsList = response.obj;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.exportData = function () {


        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "Programme Wise Marks Credit Details" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'Programme Wise Marks Credit Details|Date and Time: ' + '<br>' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'PartName', title: 'Part Name' },
                { columnid: 'PartTermName', title: 'Part Term Name' },
                { columnid: 'InstanceName', title: 'Instance Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'FollowCreditSystem', title: 'Follow Credit System' },
                { columnid: 'ProgrammePartTermMinMarks', title: 'Programme Part Term MinMarks' },
                { columnid: 'ProgrammePartTermMaxMarks', title: 'Programme Part Term MaxMarks' },
                { columnid: 'ProgrammePartMinMarks', title: 'Programme Part MinMarks' },
                { columnid: 'ProgrammePartMaxMarks', title: 'Programme Part MaxMarks' },

            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ProgrammeWiseMarksCreditDetailsList]);
    };





});