app.controller('VerifyStructureReportViewCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Verify Structure Report View";
    $scope.ProgInstList = [];
    $scope.ProgInst = {};
    $scope.ShowAfterSubmit = false;
    $scope.ShowLabel = false;     

    $scope.buildTree = function () {
        $scope.ProgInst.Id = $localStorage.Details.fullData;
        /*if ($scope.ProgInst.AcademicYearId == null || $scope.ProgInst.AcademicYearId == undefined || $scope.ProgInst.AcademicYearId == "" ||
            $scope.ProgInst.ProgrammeInstanceId == null || $scope.ProgInst.ProgrammeInstanceId == undefined || $scope.ProgInst.ProgrammeInstanceId == "" ||
            $scope.ProgInst.ProgrammeInstancePartId == null || $scope.ProgInst.ProgrammeInstancePartId == undefined || $scope.ProgInst.ProgrammeInstancePartId == "" ||
            $scope.ProgInst.SpecialisationId == null || $scope.ProgInst.SpecialisationId == undefined || $scope.ProgInst.SpecialisationId == "" ||
            $scope.ProgInst.Id == null || $scope.ProgInst.Id == undefined || $scope.ProgInst.Id == "") {
            alert("Please Select All Fields!");
        }
        else {*/
        
            $localStorage.reportData = {};
            $http({
                method: 'POST',
                url: 'api/VerifyStructureReport/GroupSelectGet',
                data: $scope.ProgInst.Id,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    }
                    else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        if (response.obj == 'Incomplete') {
                            $scope.ShowLabel = true;
                            $scope.ShowAfterSubmit = false;
                            $scope.ShowTree = false;
                          }
                    }
                    else {
                        $localStorage.reportData = response.obj;
                        $scope.FetchData();
                        $scope.ShowAfterSubmit = true;
                        $scope.ShowLabel = false;

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        //}
    };

    $scope.getRemarks = function () {

        var data = { Id: $scope.ProgInst.Id.Id };

        $http({
            method: 'POST',
            url: 'api/VerifyStructureReport/GetRemarks',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.Remarks = response.obj[0];
                $scope.ProgInst.StructureReportRemark = $scope.Remarks.StructureReportRemark;
                $scope.ProgInst.IsApprovedStructureReport = $scope.Remarks.IsApprovedStructureReport;

            })
            .error(function (res) {
                alert(res);
            });
    };

    if ($localStorage.Details) {

        $scope.ProgInst = {};        
        $scope.ProgInst.Id = $localStorage.Details.fullData;

        $scope.buildTree();
        $scope.getRemarks();

    }
    else {
        $localStorage.Details = null;
    }     

    $scope.FetchData = function () {
        $scope.GAPdata = $localStorage.reportData;
        $scope.getTreewithSelect();

    };

    $scope.getTreewithSelect = function () {
        $scope.ShowTree = true;
        
        $('#checkTree').jstree({
            'core': {
                'data': $scope.GAPdata,
                //'themes': {
                //    'responsive': false
                //}
            },
        });
        
    };    

    $scope.cancelSelection = function () {
        $scope.ProgInst = {};
        $scope.ShowTree = false;
        $scope.ShowAfterSubmit = false;
        $scope.ShowLabel = false;
    }     

    $scope.submit = function () {
        
        if ($scope.ProgInst.StructureReportRemark == null || $scope.ProgInst.StructureReportRemark == undefined || $scope.ProgInst.StructureReportRemark == "" ) {
            alert("Please Enter Faculty Remarks.");
        }
        else if ($scope.ProgInst.IsApprovedStructureReport === null || $scope.ProgInst.IsApprovedStructureReport === undefined || $scope.ProgInst.IsApprovedStructureReport === "" ) {
            alert("Please Select Approved By Faculty.");
        }
        else {

            $scope.VerifyReport = {};
            $scope.VerifyReport.StructureReportRemark = $scope.ProgInst.StructureReportRemark;
            $scope.VerifyReport.IsApprovedStructureReport = $scope.ProgInst.IsApprovedStructureReport;
            $scope.VerifyReport.Id = $scope.ProgInst.Id.Id;

            $http({
                method: 'POST',
                url: 'api/VerifyStructureReport/VerifyStructureReportEdit',
                data: $scope.VerifyReport,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    }
                    else if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.ProgInst = {};
                        $scope.ShowTree = false;
                        $scope.ShowAfterSubmit = false;
                        $scope.ShowLabel = false;
                        $state.go('VerifyStructureReport');

                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };

    $scope.backToPrevious = function () {
        $state.go('VerifyStructureReport');
        $scope.ProgInst = {};
    };

});

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

