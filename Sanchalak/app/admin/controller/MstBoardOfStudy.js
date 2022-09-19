app.controller('BOSCtrl', function ($scope, $http,$filter, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Board Of Study";   

    /*Reset BOS*/
    $scope.resetBOS = function () {
        $scope.BOS = {};
    };

    /*Get BOS List*/
    $scope.getBOSList = function () {
        
        var data = new Object();
        
        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyGet',
            
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
                    $scope.BOSTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.BOSData = response.obj;
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
            //data: $scope.Faculty,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $scope.FacultyList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Add BOS*/
    $scope.addBOS = function () {

        if ($scope.BOS.BoardName === null || $scope.BOS.BoardName === undefined ||
            $scope.BOS.FacultyId === null || $scope.BOS.FacultyId === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstBoardOfStudy/MstBoardOfStudyAdd',
                data: $scope.BOS,
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
                        $scope.BOS = {};
                        $scope.getBOSList();                        
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };    

    /*Modify BOS Data*/
    $scope.modifyMstBOSData = function (data) {
        $scope.showFormFlag = true;
        $scope.BOS = data;
        $scope.getBOSList();
        $(window).scrollTop(0);
    };

    /*Display BOS Data*/
    $scope.displayBOS = function (data) {
        $scope.BOS = data;
    };

    /*Update BOS*/
    $scope.editBOS = function () {

        if ($scope.BOS.BoardName === null || $scope.BOS.BoardName === undefined ||
            $scope.BOS.FacultyId === null || $scope.BOS.FacultyId === undefined) {

            alert("please check all fields !!!");
        }
        else {

            $http({
                method: 'POST',
                url: 'api/MstBoardOfStudy/MstBoardOfStudyEdit',
                data: $scope.BOS,
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
                        $scope.BOS = {};
                        $scope.getBOSList();
                        $scope.showFormFlag = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    /*Delete BOS*/
    $scope.deleteBOS = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.BOS = data;

            $http({
                method: 'POST',
                url: 'api/MstBoardOfStudy/MstBoardOfStudyDelete',
                data: $scope.BOS,
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
                        $scope.getBOSList();
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

    /*Active Enable BOS*/
    $scope.ShowBos = function (data) {
        
        $scope.newBos = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyIsActive',
            data: $scope.newBos,
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
                    $scope.getBOSList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable BOS*/
    $scope.HideBos = function (data) {
        
        $scope.newBos = data;

        $http({
            method: 'POST',
            url: 'api/MstBoardOfStudy/MstBoardOfStudyIsSuspended',
            data: $scope.newBos,
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
                    $scope.getBOSList();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Add New BOS*/
    $scope.newBOSAdd = function () {
        $state.go('BOSAdd');
    };

    /*Back to Edit Page of BOS*/
    $scope.backToList = function () {
        $state.go('BOSEdit');
    }; 

    $scope.exportDataofBos = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "BoardOfStudyData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Board Of Study List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'BoardName', title: 'Board Of Study Name' },               
                { columnid: 'IsActiveSts', title: 'Active Status' },
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.BOSData]);
    };

});