app.controller('mstPaperCtrl', function ($scope, $http,$filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage MstPaper Details";
    $scope.paper = {};
    
    $scope.showDefineBtn = false;

    $scope.mstPaperTableParams = new NgTableParams({
    }, {
        dataset: $scope.mstPaperGet
    });

    $scope.resetMstPaper = function () {
        $scope.paper = {};
    }

    $scope.mstPaperGet = function () {

        var data = new Object();

        $http({
            method: 'GET',
            url: 'api/MstPaper/MstPaperGet',            
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
                    $scope.mstPaperTableParams = new NgTableParams({
                    }, {
                        dataset: response.obj
                    });
                    $scope.PaperData = response.obj;
                    $scope.mstPaperGetForReport();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.mstPaperGetForReport = function () {

        var data = new Object();

        $http({
            method: 'POST',
            url: 'api/MstPaper/PaperDetailGetForReport',
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
                       $scope.PaperData1 = response.obj;
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.getEvaluation = function () {

        $http({
            method: 'POST',
            url: 'api/MstEvaluation/EvaluationGet',
            //data: $scope.paper,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.EvaluationList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getFaculty = function () {

        $http({
            method: 'POST',
            url: 'api/MstFaculty/MstFacultyGetForDropDown',
            //data: $scope.paper,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.FacultyList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getSubject = function () {

        $http({
            method: 'POST',
            url: 'api/MstSubject/MstSubjectGet',
            //data: $scope.paper,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.SubjectList = response.obj;
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getSubjectByFacultyId = function () {
        if ($scope.paper.FacultyId != null && $scope.paper.FacultyId != undefined) {
            var data = { FacultyId: $scope.paper.FacultyId };
        } else {
            alert("Something Went Wrong!!!")
        }
        $http({
            method: 'POST',
            url: 'api/MstSubject/MstSubjectGetbyFacultyId',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.SubjectList = {};
                }
                else {
                    $scope.SubjectList = response.obj;
                }
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.EnableDisableCredits = function () {
        var chkYes = document.getElementById("chkYes");
        var txtCredits = document.getElementById("credit");
        txtCredits.disabled = chkYes.checked ? false : true;
        if (!txtCredits.disabled) {
            txtCredits.focus();
        }
        else { txtCredits = null; }
    };

    $scope.addMstPaper = function () {

        var chkyes = document.getElementById("chkYes");
        var chkno = document.getElementById("chkNo");

		/*if ($scope.paper.MaxMarks < $scope.paper.MinMarks) {
            alert("Minimum Marks are not greater than Maximum Marks");
        }*/
        
        if ($scope.paper.FacultyId === null || $scope.paper.FacultyId === undefined ||
            $scope.paper.SubjectId === null || $scope.paper.SubjectId === undefined ||
            $scope.paper.PaperName === null || $scope.paper.PaperName === undefined ||
            $scope.paper.PaperCode === null || $scope.paper.PaperCode === undefined ||
            /*$scope.paper.IsCredit === null || $scope.paper.IsCredit === undefined ||*/
            $scope.paper.MaxMarks === null || $scope.paper.MaxMarks === undefined ||
            $scope.paper.MinMarks === null || $scope.paper.MinMarks === undefined ||
           /* $scope.paper.NoOfLecturesPerWeek === null || $scope.paper.NoOfLecturesPerWeek === undefined ||
            $scope.paper.IsSeparatePassingHead === null || $scope.paper.IsSeparatePassingHead === undefined ||*/
            $scope.paper.EvaluationId === null || $scope.paper.EvaluationId === undefined) {

            alert("please check all fields !!!");
        }
        else if (parseInt($scope.paper.MaxMarks) < parseInt($scope.paper.MinMarks)) {
            alert("Minimum Marks are not greater than Maximum Marks");
        }
        /*$mdDialog.show(
            $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title("Error")
                .textContent("Please complete the form before Submit...")
                .ariaLabel('Alert Dialog Demo')
                .ok('Okay!')
        );
        $scope.paper.Credits === null || $scope.paper.Credits === undefined ||*/


        else {

            $http({
                method: 'POST',
                url: 'api/MstPaper/MstPaperAdd',
                data: $scope.paper,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {

                    $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        //Redirect user to login page
                        $state.go('login');
                        //return false;
                    } else if (response.response_code != "200") {
                        // alert(response.obj);
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {

                        $localStorage.define = {};
                        $localStorage.define.PaperId = response.obj.Item2;
                        $localStorage.define.PaperName = response.obj.Item3;
                        $localStorage.define.PaperCode = response.obj.Item4;
                        $localStorage.define.Credits = response.obj.Item5;
                        $localStorage.define.MaxMarks = response.obj.Item6;
                        $scope.mstPaperGet();
                        //$scope.paper = {};
                        console.log("Paper: ------");
                        console.log($localStorage.define);
                        alert(response.obj.Item1);
                        $scope.showDefineBtn = true;
                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }

    };

    $scope.editMstPaper = function () {

        if ($scope.paper.FacultyId === null || $scope.paper.FacultyId === undefined ||
            $scope.paper.SubjectId === null || $scope.paper.SubjectId === undefined ||
            $scope.paper.PaperName === null || $scope.paper.PaperName === undefined ||
            $scope.paper.PaperCode === null || $scope.paper.PaperCode === undefined ||
            /*$scope.paper.IsCredit === null || $scope.paper.IsCredit === undefined ||*/
            $scope.paper.MaxMarks === null || $scope.paper.MaxMarks === undefined ||
            $scope.paper.MinMarks === null || $scope.paper.MinMarks === undefined ||
            /*$scope.paper.NoOfLecturesPerWeek === null || $scope.paper.NoOfLecturesPerWeek === undefined ||
            $scope.paper.IsSeparatePassingHead === null || $scope.paper.IsSeparatePassingHead === undefined ||*/
            $scope.paper.EvaluationId === null || $scope.paper.EvaluationId === undefined) {

            alert("please check all fields !!!");
        }
        else if (parseInt($scope.paper.MaxMarks) < parseInt($scope.paper.MinMarks)) {
            alert("Minimum Marks are not greater than Maximum Marks");
        }
        else {
            $http({
                method: 'POST',
                url: 'api/MstPaper/MstPaperEdit',
                data: $scope.paper,
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
                        $scope.showFormFlag = false;
                        $scope.mstPaperGet();

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.deleteMstPaper = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.paper = data;

            $http({
                method: 'POST',
                url: 'api/MstPaper/MstPaperDelete',
                data: $scope.paper,
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
                        $scope.mstPaperGet();
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

    $scope.deleteMstPaperTeachingLearingMap = function (ev, data) {
        
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');

        $mdDialog.show(confirm).then(function () {
            $scope.paper = data;

            $http({
                method: 'POST',
                url: 'api/MstPaper/MstPaperTLMAMDelete',
                data: $scope.paper,
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
                        $scope.mstPaperGet();
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

    $scope.displayPaper = function (data) {
        $http({
            method: 'POST',
            url: 'api/MstPaper/MstPaperDetailGet',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    data = response.obj;
                    $scope.PaperData1 = response.obj;
                    if (data.PaperTLMMapData.length <= 0) {
                        $scope.showTLMMap = false;
                        $scope.paper = data;
                    }
                    else {
                        $scope.showTLMMap = true;
                        $scope.paper = data;
                        
                    }
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.modifyMstPaperData = function (data) {

        $scope.showFormFlag = true;
        $scope.paper = data;
        if (!($scope.getFaculty == null && $scope.getFaculty == undefined)) { $scope.getSubjectByFacultyId(); }
        //$scope.getSubject();        
    };

    $scope.ShowPaper = function (data) {
        $scope.newpaper = data;

        $http({
            method: 'POST',
            url: 'api/MstPaper/MstPaperIsActive',
            data: $scope.newpaper,
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
                    $scope.mstPaperGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.HidePaper = function (data) {

        $scope.newpaper = data;

        $http({
            method: 'POST',
            url: 'api/MstPaper/MstPaperIsSuspended',
            data: $scope.newpaper,
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
                    $scope.mstPaperGet();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    $scope.redirectToEdit = function (data) {
        $rootScope.paper = data;
        $state.go('mstPaperEdit');
    };    

    $scope.newPaperAdd = function () {
        $state.go('mstPaperAdd');
    };

    $scope.backToList = function () {
        $state.go('mstPaperEdit');
    };

    $scope.GotoPaperTLMMap = function () {
        //$localStorage.define = {};
        //$localStorage.define.PaperId = $scope.paper.Id;
        //$localStorage.define.PaperName = $scope.paper.PaperName;
        console.log($localStorage.define);
        $localStorage.TLMEdit = {};
        $state.go('PaperTLMMapAdd');
    };

    $scope.GotoPaperTLMMapEdit = function () {
        //$localStorage.TLMEdit = {};
        $localStorage.define = {};
        $localStorage.define.PaperId = $scope.paper.Id;
        $localStorage.define.PaperName = $scope.paper.PaperName;
        $localStorage.define.PaperCode = $scope.paper.PaperCode;
        $localStorage.define.Credits = $scope.paper.Credits;
        $localStorage.define.MaxMarks = $scope.paper.MaxMarks;
        console.log($localStorage.define);
        $state.go('PaperTLMMapAdd');
    };      

    $scope.exportDataofPaper = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PaperData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Paper List | Date and Time: ' + DateAndTime,
            },
            columns: [
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'FacultyName', title: 'Faculty Name' },
                { columnid: 'SubjectName', title: 'Subject Name' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'PaperCode', title: 'Paper Code' },
                { columnid: 'IsCredit', title: 'Is Credit' },
                { columnid: 'MaxMarks', title: 'Max Marks' },
                { columnid: 'MinMarks', title: 'Min Marks' },
                { columnid: 'Credits', title: 'Credits' },
                { columnid: 'IsSeparatePassingHead', title: 'Is Separate Passing Head' },
                { columnid: 'EvaluationName', title: 'Evaluation Name' },
                /*{ columnid: 'TeachingLearningMethodName', title: 'Teaching Learning Method Name' },
                { columnid: 'AssessmentMethodName', title: 'Assessment Method Name' },
                { columnid: 'NoOfCredits', title: 'No Of Credits' },
                { columnid: 'NoOfHoursPerWeek', title: 'No Of Hours Per Week' },
                { columnid: 'AssessmentMethodMarks', title: 'Assessment Method Marks' },
                { columnid: 'AssessmentType', title: 'Assessment Type' },
                { columnid: 'AssessmentTypeMaxMarks', title: 'Assessment Type Max Marks' },
                { columnid: 'AssessmentTypeMinMarks', title: 'Assessment Type Min Marks' },*/
                { columnid: 'IsActiveSts', title: 'Active Status' },
            ],
        };
        
        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.PaperData]);
    };
    
    $scope.exportDataofPaperMap = function () {

        alert("Please wait, Excel is being prepared...");

        var LongDate = new Date($.now());
        var ShortDate = $filter('date')(LongDate, "yyyy-MM-dd");
        var DateWithoutDashed = ShortDate.replace(new RegExp('-', 'g'), "");
        var time = "_" + LongDate.getHours() + LongDate.getMinutes() + LongDate.getSeconds();

        var DateAndTime = ShortDate + " " + LongDate.getHours() + ":" + LongDate.getMinutes();
        var ExcelFileName = "PaperMapData_" + DateWithoutDashed + time;

        var mystyle = {
            headers: true,
            style: 'font-size:25px;font-weight:bold',
            caption: {
                title: 'Paper Map List | Date and Time: ' + DateAndTime,
            },
            columns: [   
                { columnid: 'IndexId', title: 'Sr.No' },
                { columnid: 'PaperName', title: 'Paper Name' },
                { columnid: 'PaperCode', title: 'Paper Code' },                
                { columnid: 'TeachingLearningMethodName', title: 'Teaching Learning Method Name' },
                { columnid: 'AssessmentMethodName', title: 'Assessment Method Name' },
                { columnid: 'NoOfCredits', title: 'No Of Credits' },
                { columnid: 'NoOfHoursPerWeek', title: 'No Of Hours Per Week' },
                { columnid: 'AssessmentMethodMarks', title: 'Assessment Method Marks' },
                { columnid: 'AssessmentType', title: 'Assessment Type' },
                { columnid: 'AssessmentTypeMaxMarks', title: 'Assessment Type Max Marks' },
                { columnid: 'AssessmentTypeMinMarks', title: 'Assessment Type Min Marks' },                
            ],
        };
        
        //Create XLS format using alasql.js file.
        alasql('SELECT * INTO XLS("' + ExcelFileName + '.xls",?) FROM ?', [mystyle, $scope.PaperData1]);
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