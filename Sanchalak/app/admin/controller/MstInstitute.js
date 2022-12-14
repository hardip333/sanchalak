app.controller('MstInstituteCtrl', function ($scope, $http, $rootScope,$filter, $state, $cookies, $mdDialog, NgTableParams) {
   
    $rootScope.pageTitle = "Manage MstInstitute";

    /*Reset Institute*/
    $scope.resetInstitute = function () {
        $scope.MstInstitute = {};
    };

    /*Get Institute List*/
    $scope.getInstitute = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteGet',
            
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
                    $scope.MstInstituteTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.InstituteData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add Institute*/
    $scope.addInstitute = function () {

        if ($scope.MstInstitute.InstituteName === null || $scope.MstInstitute.InstituteName === undefined ||
            $scope.MstInstitute.InstituteCode === null || $scope.MstInstitute.InstituteCode === undefined ) {

            alert("please check Institute name or code fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstInstitute/MstInstituteAdd',
                data: $scope.MstInstitute,
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
                        $scope.MstInstitute = {};
                        $scope.getInstitute();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Modify Institute Data*/
    $scope.modifyInstituteData = function (data) {
        $scope.MstInstitute = data;
        $scope.showFormFlag = true;
        $(window).scrollTop(0);
    };

    /*Update Institute*/
    $scope.editInstitute = function () {

        if ($scope.MstInstitute.InstituteName === null || $scope.MstInstitute.InstituteName === undefined ||
            $scope.MstInstitute.InstituteCode === null || $scope.MstInstitute.InstituteCode === undefined) {

            alert("please check Institute name or code fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstInstitute/MstInstituteUpdate',
                data: $scope.MstInstitute,
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
                        $scope.getInstitute();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete Institute*/
    $scope.deleteInstitute = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstInstitute = data;

            $http({
                method: 'POST',
                url: 'api/MstInstitute/MstInstituteDelete',
                data: $scope.MstInstitute,
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
                        $scope.getInstitute();
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            
        }, function () {
            $scope.status = 'You decided to keep your debt.';
        });
    };       

    /*Add New Institute*/
    $scope.newInstituteAdd = function () {
        $state.go('MstInstituteAdd');
    };

    /*Back to Edit Page of Institute*/
    $scope.backToList = function () {
        $state.go('MstInstituteEdit');
    };

    /*Display Institute Data*/
    $scope.displayInstitute = function (data) {
        $scope.MstInstitute = data;
    };

    /*Active Enable Institute*/
    $scope.ShowInstitute = function (data) {

        $scope.MstInstitute = data;

        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteIsActive',
            data: $scope.MstInstitute,
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
                    $scope.getInstitute();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable Institute*/
    $scope.HideInstitute = function (data) {

        $scope.MstInstitute = data;

        $http({
            method: 'POST',
            url: 'api/MstInstitute/MstInstituteIsSuspended',
            data: $scope.MstInstitute,
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
                    $scope.getInstitute();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.exportDataofInstitute = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "InstituteData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Institute List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'InstituteName', title: 'Institute Name' },
                { columnid: 'InstituteCode', title: 'Institute Code' },
                { columnid: 'InstituteAddress', title: 'Institute Address' },
                { columnid: 'CityName', title: 'City Name' },
                { columnid: 'Pincode', title: 'Pin-code' },
                { columnid: 'InstituteContactNo', title: 'Institute Contact No.' },
                { columnid: 'InstituteFaxNo', title: 'Institute Fax No.' },
                { columnid: 'InstituteEmail', title: 'Institute Email' },
                { columnid: 'InstituteUrl', title: 'Institute URL' },
                { columnid: 'IsActiveSts', title: 'Active Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.InstituteData]);
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





    

