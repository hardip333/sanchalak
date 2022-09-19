app.controller('MstProgrammePartTermCtrl', function ($scope, $http,$filter,$localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {
    
    $rootScope.pageTitle = "Manage MstProgrammePartTerm";

    /*Check Values in LocalStorage*/
    if ($localStorage.define) {
        $scope.MstProgrammePartTerm = {};
        $scope.MstProgrammePartTerm.FacultyId = $localStorage.define.faculty;
        $scope.MstProgrammePartTerm.ProgrammeId = $localStorage.define.ProgrammeId;
        $scope.MstProgrammePartTerm.PartId = $localStorage.define.ProgrammePartId;
    }

    /*Reset ProgrammePartTerm*/
    $scope.resetMstProgrammePartTerm = function () {
        $scope.MstProgrammePartTerm = {};
        $localStorage.define = {};    
    };

    /*Get ProgrammePartTerm List*/
    $scope.MstProgrammePartTermListGet = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTerm/MstProgrammePartTermListGet',
            
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
                    $scope.MstProgrammePartTermTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.ProgrammePartTermData = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };      

    /*Get Faculty List*/
    $scope.FacultyGet = function () {
        
        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',
            //data: SubSpecialisation,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacList = {};
                }
                else {
                    $scope.FacList = response.obj;
                    
                    if ($localStorage.define.ProgrammeId) {
                        $scope.getProgrammeListByFacId();
                    }
                }
              
            })
            .error(function (res) {
                
            });
    }; 

    /*Get Programme List By Fac Id*/
    $scope.getProgrammeListByFacId = function () {
       
        if ($scope.MstProgrammePartTerm.FacultyId != null && $scope.MstProgrammePartTerm.FacultyId != undefined) {
            var data = { FacultyId: $scope.MstProgrammePartTerm.FacultyId };
        } else {
            alert("Something Went Wrong!!!");
        }   

        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/MstProgrammeGetByFacId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgrammeList1 = {};
                }
                else {
                    $scope.ProgrammeList1 = response.obj;
                    if ($localStorage.define.ProgrammePartId) {
                        $scope.MstProgrammePartGet();
                    }
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    /*Get Programme List By Faculty/Inst Id*/
    $scope.MstProgrammeGet = function () {
        if ($scope.MstProgrammePartTerm.FacultyId != null && $scope.MstProgrammePartTerm.FacultyId != undefined) {
            var data = { FacultyId: $scope.MstProgrammePartTerm.FacultyId };
        } else {
            alert("Something Went Wrong!!!");
        }        
        
        $http({
            method: 'POST',
            url: 'api/MstProgrammePart/MstProgrammeGetByFacultyId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })

            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgList = {};
                }
                else {
                    $scope.ProgList = response.obj;
                    if ($localStorage.define.ProgrammePartId) {
                        $scope.MstProgrammePartGet();
                    }
                }

            })
            .error(function (res) {
               
            });
    };      

    /*Get ProgrammePart List*/
    $scope.MstProgrammePartGet = function () {
        var data = { ProgrammeId: $scope.MstProgrammePartTerm.ProgrammeId };
        
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTerm/MstProgrammePartGetByProgrammeId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.ProgPartList = {};
                }
                else {
                    $scope.ProgPartList = response.obj;
                }

            })
            .error(function (res) {
                
            });
    };   

    /*Add ProgrammePartTerm*/
    $scope.MstProgrammePartTermAdd = function () {

        if ($scope.MstProgrammePartTerm.PartId === null || $scope.MstProgrammePartTerm.PartId === undefined ||
            $scope.MstProgrammePartTerm.PartTermName === null || $scope.MstProgrammePartTerm.PartTermName === undefined ||
            $scope.MstProgrammePartTerm.PartTermShortName === null || $scope.MstProgrammePartTerm.PartTermShortName === undefined ||
            $scope.MstProgrammePartTerm.SequenceNo === null || $scope.MstProgrammePartTerm.SequenceNo === undefined) { alert("Enter All Fields"); }
        else {

            $http({
                method: 'POST',
                url: 'api/MstProgrammePartTerm/MstProgrammePartTermAdd',
                data: $scope.MstProgrammePartTerm,
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
                        $localStorage.define = {};    
                        $scope.MstProgrammePartTerm = {};
                        $scope.MstProgrammePartTermListGet();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
    };

    /*Modify ProgrammePartTerm Data*/
    $scope.modifyMstProgrammePartTermData = function (data) {

        $scope.showFormFlag = true;
        $scope.MstProgrammePartTerm = data;
        if (!($scope.FacultyGet == null && $scope.FacultyGet == undefined)) { $scope.getProgrammeListByFacId(); }
        if (!($scope.getProgrammeListByFacId == null && $scope.getProgrammeListByFacId == undefined)) { $scope.MstProgrammePartGet(); }

    };

    /*Update ProgrammePartTerm*/
    $scope.MstProgrammePartTermUpdate = function () {
       
        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTerm/MstProgrammePartTermUpdate',
            data: $scope.MstProgrammePartTerm,
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
                    $scope.showFormFlag = false;
                    $scope.MstProgrammePartTermListGet();
                    $state.go('UpdateMstProgrammePartTerm');
                    

                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Delete ProgrammePartTerm*/
    $scope.MstProgrammePartTermDelete = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.MstProgrammePartTerm = data;

            $http({
                method: 'POST',
                url: 'api/MstProgrammePartTerm/MstProgrammePartTermDelete',
                data: $scope.MstProgrammePartTerm,
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
                        $scope.MstProgrammePartTermListGet();
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

    /*Add New ProgrammePartTerm*/
    $scope.newMstProgrammePartTermAdd = function () {
        $state.go('ProgrammePartTermAdd');
    };

    /*Back to Edit Page of ProgrammePartTerm*/
    $scope.backToList = function () {
        $localStorage.define = {};    
        $state.go('ProgrammePartTermEdit');        
    }; 

    /*Display ProgrammePartTerm Data*/
    $scope.displayMstProgrammePartTerm = function (data) {
        $scope.MstProgrammePartTerm = data;
    };

    /*Active Enable ProgrammePartTerm*/
    $scope.ShowProgrammePartTerm = function (data) {

        $scope.newMstProgrammePartTerm = data;

        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTerm/MstProgrammePartTermIsActive',
            data: $scope.newMstProgrammePartTerm,
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
                    $scope.MstProgrammePartTermListGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Active Disable ProgrammePartTerm*/
    $scope.HideProgrammePartTerm = function (data) {

        $scope.newMstProgrammePartTerm = data;

        $http({
            method: 'POST',
            url: 'api/MstProgrammePartTerm/MstProgrammePartTermIsSuspended',
            data: $scope.newMstProgrammePartTerm,
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
                    $scope.MstProgrammePartTermListGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.exportDataofProgrammePartTerm = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "ProgrammePartTermData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Programme Part Term List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'ProgrammeName', title: 'Programme Name' },
                { columnid: 'PartTermName', title: 'Programme Part Term Name' },
                { columnid: 'PartTermShortName', title: 'Programme Part Term Short Name' },                
            ],
        };

        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.ProgrammePartTermData]);
    };

});

/*$scope.MstProgrammePartTermTableParams = new NgTableParams({
    }, {
            dataset: $scope.MstProgrammePartTermListGet
    });*/