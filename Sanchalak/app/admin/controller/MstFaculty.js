app.controller('FacultyCtrl', function ($scope, $http, $rootScope, $state, $cookies, $mdDialog, NgTableParams, $filter) {

    $rootScope.pageTitle = "Manage Faculty";

    /*Reset Faculty*/
    $scope.resetFaculty = function () {
        $scope.Faculty = {};
    };

    /*Get Faculty List*/
    $scope.getFacultyList = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGet',
            
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
                    $scope.FacultyTableParams = new NgTableParams({}, { dataset: response.obj });
                    $scope.FacultyData = response.obj;
                }

            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Faculty*/
    $scope.addFaculty = function () {

        if ($scope.Faculty.FacultyName === null || $scope.Faculty.FacultyName === undefined ||
            $scope.Faculty.FacultyCode === null || $scope.Faculty.FacultyCode === undefined /*||
            $scope.Faculty.FacultyAddress === null || $scope.Faculty.FacultyAddress === undefined ||
            $scope.Faculty.CityName === null || $scope.Faculty.CityName === undefined ||
            $scope.Faculty.Pincode === null || $scope.Faculty.Pincode === undefined ||
            $scope.Faculty.FacultyContactNo === null || $scope.Faculty.FacultyContactNo === undefined ||
            $scope.Faculty.FacultyFaxNo === null || $scope.Faculty.FacultyFaxNo === undefined ||
            $scope.Faculty.FacultyEmail === null || $scope.Faculty.FacultyEmail === undefined ||
            $scope.Faculty.FacultyUrl === null || $scope.Faculty.FacultyUrl === undefined*/ ) {

            alert("please check faculty name and code fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstFaculty/MstFacultyAdd',
                data: $scope.Faculty,
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
                        alert(response.obj);                                                                    
                        $scope.getFacultyList();
                        $scope.Faculty = {};

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };   

    /*Modify Faculty Data*/
    $scope.modifyMstFacultyData = function (data) {
        $scope.showFormFlag = true;
        $scope.Faculty = data;
        $(window).scrollTop(0);
    };

    /*Display Faculty Data*/
    $scope.displayFaculty = function (data) {
        $scope.Faculty = data;
    };

    /*Update Faculty*/
    $scope.editFaculty = function () {

        if ($scope.Faculty.FacultyName === null || $scope.Faculty.FacultyName === undefined ||
            $scope.Faculty.FacultyCode === null || $scope.Faculty.FacultyCode === undefined /*||
            $scope.Faculty.FacultyAddress === null || $scope.Faculty.FacultyAddress === undefined ||
            $scope.Faculty.CityName === null || $scope.Faculty.CityName === undefined ||
            $scope.Faculty.Pincode === null || $scope.Faculty.Pincode === undefined ||
            $scope.Faculty.FacultyContactNo === null || $scope.Faculty.FacultyContactNo === undefined ||
            $scope.Faculty.FacultyFaxNo === null || $scope.Faculty.FacultyFaxNo === undefined ||
            $scope.Faculty.FacultyEmail === null || $scope.Faculty.FacultyEmail === undefined ||
            $scope.Faculty.FacultyUrl === null || $scope.Faculty.FacultyUrl === undefined*/) {

            alert("please check faculty name and code fields!!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstFaculty/MstFacultyEdit',
                data: $scope.Faculty,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj); 
                        $scope.resetFaculty();
                        $scope.getFacultyList();
                        $scope.showFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    }; 

    /*Delete Faculty*/
    $scope.deleteFaculty = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.Faculty = data;

            $http({
                method: 'POST',
                url: 'api/MstFaculty/MstFacultyDelete',
                data: $scope.Faculty,
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
                        alert(response.obj); 
                        $scope.getFacultyList();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            // };
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };

    /*Active Enable Faculty*/
    $scope.ShowFaculty = function (data) {
        
        $scope.newfaculty = data;

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyIsActive',
            data: $scope.newfaculty,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj); 
                    $scope.getFacultyList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Faculty*/
    $scope.HideFaculty= function (data) {
        
        $scope.newfaculty = data;

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyIsSuspended',
            data: $scope.newfaculty,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    alert(response.obj); 
                    $scope.getFacultyList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add New Faculty*/
    $scope.newFacultyAdd = function () {
        $state.go('FacultyAdd');
    };

    /*Back to Edit Page of Faculty*/
    $scope.backToList = function () {
        $state.go('FacultyEdit');
    };  

    $scope.exportDataofFaculty = function () {

        alert("Please wait, Excel is being prepared...");

        /*if ($scope.searchCaseResult == undefined) {
            alert("Please select Programme Instance Part Term then click on Search");
            return false;
        }*/
        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "FacultyData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Faculty List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'FacultyCode', title: 'Faculty Code' },
                { columnid: 'FacultyAddress', title: 'Faculty Address' },
                { columnid: 'CityName', title: 'City Name' },
                { columnid: 'Pincode', title: 'Pin-code' },
                { columnid: 'FacultyContactNo', title: 'Faculty Contact No.' },
                { columnid: 'FacultyFaxNo', title: 'Faculty Fax No.' },
                { columnid: 'FacultyEmail', title: 'Faculty Email' },
                { columnid: 'FacultyUrl', title: 'Faculty URL' },
                { columnid: 'IsActiveSts', title: 'Active Status' },                
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.FacultyData]);
    };      

      
});

app.directive('allowPattern', [allowPatternDirective]);

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}