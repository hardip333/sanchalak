app.controller('MstSpecialisationCtrl', function ($scope, $http,$filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    
    $rootScope.pageTitle = "Manage MstSpecialisation";

    /*Reset Specialisation*/
    $scope.resetSpecialisation = function () {
        $scope.MstSpecialisation = {};
    };

    /*Get Specialisation List*/
    $scope.getSpecialisation = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstSpecialisation/MstSpecialisationGet',
            
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
                    $scope.MstSpecialisationTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.SpecialisationData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Faculty List*/
    $scope.getFacultyList = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',
            //data: $scope.MstSpecialisation,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                $scope.FacultyList = response.obj;

            })
            .error(function (res) {
                
            });
    };   

    /*Add Specialisation*/
    $scope.addSpecialisation = function () {
        if ($scope.MstSpecialisation.FacultyId === null || $scope.MstSpecialisation.FacultyId === undefined ||
            $scope.MstSpecialisation.BranchName === null || $scope.MstSpecialisation.BranchName === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/MstSpecialisation/MstSpecialisationAdd',
                data: $scope.MstSpecialisation,
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
                        $scope.MstSpecialisation = {};
                        $scope.getSpecialisation();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
    };

    /*Modify Specialisation Data*/
    $scope.modifySpecialisationData = function (data) {

        $scope.showFormFlag = true;
        $scope.MstSpecialisation = data;
        $(window).scrollTop(0);
    };

    /*Update Specialisation*/
    $scope.editSpecialisation = function () {        
        
        if ($scope.MstSpecialisation.FacultyId === null || $scope.MstSpecialisation.FacultyId === undefined ||
            $scope.MstSpecialisation.BranchName === null || $scope.MstSpecialisation.BranchName === undefined) { alert("Enter All Fields"); }
        else {
            $http({
                method: 'POST',
                url: 'api/MstSpecialisation/MstSpecialisationUpdate',
                data: $scope.MstSpecialisation,
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
                        $scope.getSpecialisation();
                        $scope.showFormFlag = false;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
        
    };

    /*Delete Specialisation*/
    $scope.deleteSpecialisation = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstSpecialisation = data;

            $http({
                method: 'POST',
                url: 'api/MstSpecialisation/MstSpecialisationDelete',
                data: $scope.MstSpecialisation,
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
                        $scope.getSpecialisation();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };   

    /*Add New Specialisation*/
    $scope.newSpecialisationAdd = function () {
        $state.go('SpecialisationAdd');
    };

    /*Back to Edit Page of Specialisation*/
    $scope.backToList = function () {
        $state.go('SpecialisationEdit');
    };

    /*Display Specialisation Data*/
    $scope.displaySpecialisation = function (data) {
        $scope.MstSpecialisation = data;
    };

    /*Active Enable Specialisation*/
    $scope.ShowSpecialisation = function (data) {

        $scope.MstSpecialisation = data;

        $http({
            method: 'POST',
            url: 'api/MstSpecialisation/MstSpecialisationIsActive',
            data: $scope.MstSpecialisation,
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
                    $scope.getSpecialisation();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Specialisation*/
    $scope.HideSpecialisation = function (data) {

        $scope.MstSpecialisation = data;

        $http({
            method: 'POST',
            url: 'api/MstSpecialisation/MstSpecialisationIsSuspended',
            data: $scope.MstSpecialisation,
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
                    $scope.getSpecialisation();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };
    
    $scope.exportDataofSpecialisation = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "SpecialisationData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Specialisation List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'BranchName', title: 'Branch Name' },                
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.SpecialisationData]);
    };

});