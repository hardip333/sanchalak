app.controller('FacultyWiseCategoryReportCtrl', function ($scope, $http, $rootScope, $filter, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Time Table Conflicts Report";
    $scope.FacultyCategory = {};

    //Spinner ON
    $scope.onSpinner = function on() {
        document.getElementById("overlay").style.display = "flex";
    }
    //Spinner OFF
    $scope.offSpinner = function off() {
        document.getElementById("overlay").style.display = "none";
    }

    $scope.getFacultyList = function () {

        $http({
            method: 'POST',
            url: 'api/FacultyWiseCategoryReport/MstFacultyGet',
            data: $scope.FacultyCategory,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getAcademicList = function () {
        $http({
            method: 'POST',
            url: 'api/FacultyWiseCategoryReport/AcademicYearGet',
            // data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.AcadList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };


    $scope.getAcademicWiseCategoryReport = function () {


        if ($scope.FacultyCategory.FacultyId == null || $scope.FacultyCategory.FacultyId == undefined 
            
        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Faculty DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.FacultyCategory.AcademicYearId == null || $scope.FacultyCategory.AcademicYearId == undefined || $scope.FacultyCategory.AcademicYearId == "")
        {
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
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/FacultyWiseCategoryReport/FacultyWiseCategoryReportGetbyFId',
                data: $scope.FacultyCategory,
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
                        $scope.FacultyWiseCategoryReport = response.obj;

                        $scope.IsTableVisible = true;
                        $scope.IsExcelButton = true;
                        $scope.NoRecLabel = false;
                        $scope.FacultyWiseCategoryReportTableParams = new NgTableParams({
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

    $scope.cancelAcademicCategoryWiseReport = function () {
        $scope.FacultyCategory = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
        $scope.NoRecLabel = false;
    };

 

    $scope.exportDataAcademic = function () {


        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "AcademicWiseCategoryReport" + DateWithoutDashed + time;
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'Academic Wise Category Report |Date and Time: ' + '<br>' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'ProgrammeCode', title: 'Programme Code' },
                { columnid: 'ProgrammeModeName', title: 'Programme Mode Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'PartName', title: 'Part Name' },
                { columnid: 'PartTermName', title: 'Part Term Name' },
                { columnid: 'TotalStudent', title: 'Total Student' },
                { columnid: 'FemalePH', title: 'Physically Challenged-Female' },
                { columnid: 'FemaleGeneral', title: 'Female-General' },
                { columnid: 'FemaleSC', title: 'Female-SC' },
                { columnid: 'FemaleST', title: 'Female-ST' },
                { columnid: 'FemaleSEBC', title: 'Female-SEBC' },
                { columnid: 'TotalFemale', title: 'Total Female' },

                { columnid: 'MalePH', title: 'Physically Challenged - Male' },
                { columnid: 'MaleGeneral', title: 'Male General' },
                { columnid: 'MaleSC', title: 'Male SC' },
                { columnid: 'MaleST', title: 'Male ST' },
                { columnid: 'MaleSEBC', title: 'Male SEBC' },
                { columnid: 'TotalMale', title: 'TotalMale' },







            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.FacultyWiseCategoryReport]);
    };

    $scope.getFacultyById = function () {

        $http({
            method: 'POST',
            url: 'api/FacultyWiseCategoryReport/MstFacultyGetbyId',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyCategory = response.obj[0];
            
               
                
            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.cancelFacultyCategoryWiseReport = function () {
        $scope.FacultyCategory.AcademicYearId = {};
        $scope.IsTableVisible = false;
        $scope.IsExcelButton = false;
        $scope.NoRecLabel = false;
    };

    $scope.getFacultyWiseCategoryReport = function () {
        $scope.FacultyCategory.FacultyId = $scope.FacultyCategory.Id;
        if ($scope.FacultyCategory.FacultyId == null || $scope.FacultyCategory.FacultyId == undefined

        ) {

            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Select Faculty DropDown Value")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else if ($scope.FacultyCategory.AcademicYearId == null || $scope.FacultyCategory.AcademicYearId == undefined || $scope.FacultyCategory.AcademicYearId == "") {
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
        else {
            $scope.onSpinner();
            $http({
                method: 'POST',
                url: 'api/FacultyWiseCategoryReport/FacultyWiseCategoryReportGetbyFId',
                data: $scope.FacultyCategory,
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
                        $scope.FacultyWiseCategoryReport = response.obj;

                        $scope.IsTableVisible = true;
                        $scope.IsExcelButton = true;
                        $scope.NoRecLabel = false;
                        $scope.FacultyWiseCategoryReportTableParams = new NgTableParams({
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
    $scope.exportDataFaculty = function () {


        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();
        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "FacultyWiseCategoryReport" + DateWithoutDashed + time;
       
        var mystyle = {
            headers: true,
            style: 'font-size:20px;font-weight:bold',
            caption: {
                title: 'Faculty Wise Category Report |Date and Time: ' + '<br>' + DateAndTime,
            },
           
            columns: [
                { columnid: 'IndexId', title: 'Sr.No'},
             
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'ProgrammeCode', title: 'Programme Code' },
                { columnid: 'ProgrammeModeName', title: 'Programme Mode Name' },
                { columnid: 'BranchName', title: 'Branch Name' },
                { columnid: 'InstancePartTermName', title: 'Instance Part Term Name' },
                { columnid: 'PartName', title: 'Part Name' },
                { columnid: 'PartTermName', title: 'Part Term Name' },
                { columnid: 'TotalStudent', title: 'Total Student' },

                { columnid: 'FemalePH', title: 'Physically Challenged-Female' },
                { columnid: 'FemaleGeneral', title: 'Female-General' },
                { columnid: 'FemaleSC', title: 'Female-SC' },
                { columnid: 'FemaleST', title: 'Female-ST' },
                { columnid: 'FemaleSEBC', title: 'Female-SEBC' },
                { columnid: 'TotalFemale', title: 'Total Female' },

                { columnid: 'MalePH', title: 'Physically Challenged - Male' },
                { columnid: 'MaleGeneral', title: 'Male General' },
                { columnid: 'MaleSC', title: 'Male SC' },
                { columnid: 'MaleST', title: 'Male ST' },
                { columnid: 'MaleSEBC', title: 'Male SEBC' },
                { columnid: 'TotalMale', title: 'TotalMale' },
               
               
                    
              




            ],

           
        };
     
        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.FacultyWiseCategoryReport]);
    };


});