app.controller('ProgrammeWisePaperTLMDetailsCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Programme Wise Paper TLM Details Report";
    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    /*CancelCenterWiseBlockReport*/
    $scope.resetProgrammeWisePaperTLMList = function () {
        $scope.ProgPaperTLM = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
    };

    $scope.MstFacultyGet = function () {
        $scope.FacultyList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeWisePaperTLMDetails/MstFacultyGetForDropDown',
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

    $scope.MstInstituteGetByFacultyId = function () {

        $scope.InstituteList = {};
        $http({
            method: 'POST',
            url: 'api/ProgrammeWisePaperTLMDetails/InstituteGetByFacultyId',
            data: $scope.ProgPaperTLM,
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
            url: 'api/ProgrammeWisePaperTLMDetails/IncAcademicYearGet',
            data: $scope.ProgPaperTLM,
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


    $scope.getProgrammeWisePaperTLMList = function () {
        if ($scope.ProgPaperTLM.FacultyId === null || $scope.ProgPaperTLM.FacultyId === undefined ||
            $scope.ProgPaperTLM.InstituteId === null || $scope.ProgPaperTLM.InstituteId === undefined ||
            $scope.ProgPaperTLM.AcademicYearId === null || $scope.ProgPaperTLM.AcademicYearId === undefined) {
            alert("please check all fields !!!");
        }
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/ProgrammeWisePaperTLMDetails/ProgrammeWisePaperTLMDetailsById',
                data: $scope.ProgPaperTLM,
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
                        $scope.ProgrammeWisePaperTLMTableParams = new NgTableParams({
                        }, {
                            dataset: response.obj
                        });
                        $scope.ProgrammeWisePaperTLMList = response.obj;
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
        var ExcelFileName = "Programme Wise Paper TLM List" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'Programme Wise Paper TLM List|Date and Time: ' + '<br>' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'PartName', title: 'Part Name' },
                { columnid: 'PartTermName', title: 'Part Term Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'PaperCredit', title: 'Paper Credit' },
                { columnid: 'MaxMarks', title: 'Max Marks' },
                { columnid: 'MinMarks', title: 'Min Marks' },
                { columnid: 'TLM', title: 'TLM' },
                { columnid: 'AM', title: 'AM' },
                { columnid: 'AssessmentMethodMarks', title: 'Assessment Method Marks' },
                { columnid: 'FollowCreditSystem', title: 'Follow Credit System' },
                { columnid: 'AssessmentType', title: 'Assessment Type' },
                { columnid: 'AssessmentTypeMaxMarks', title: 'Assessment Type Max Marks' },
                { columnid: 'AssessmentTypeMinMarks', title: 'Assessment Type Min Marks' },
               
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ProgrammeWisePaperTLMList]);
    };





});