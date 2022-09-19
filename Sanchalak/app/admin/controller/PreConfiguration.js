app.controller('PreConfigurationCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {
    $rootScope.pageTitle = "Programme Part Term Detail";
    $scope.ProgInstList = [];
    $scope.ProgInst = {};
    $scope.ProgInstPartTermTableparam = new NgTableParams(
        {}, {
        dataset: $scope.getProgInstList
    });

    $scope.getPreProgInsName = function () {
        $scope.ProgrammeInstanceName = $localStorage.localObj.ProgInstanceName;
        $scope.ProgrammeName = $localStorage.localObj.ProgrammeName;
        $scope.PartName = $localStorage.localObj.PartName;
        $scope.ProgrammePartTermName = $localStorage.localObj.ProgrammePartTermName;
        $state.go('PreConfiguration');
    };

    $scope.nextbtnEligibilityGroup = function () {
        $state.go('AdmEligibilityGroupEdit');
    };
    $scope.nextbtnEligibilityGroupComponent = function () {
        $state.go('AdmEligibilityGroupComponentEdit');
    };
    $scope.nextbtnProgrammeAddOn = function () {
        $state.go('AdmProgrammeAddOnCriteriaEdit');
    };
    $scope.nextbtnRequiredDocument = function () {
        $state.go('AdmRequiredDocumentsProgramEdit');
    };
    $scope.nextbtnApplicationConfiguration = function () {
        $state.go('PreApplicationConfigurationEdit');
    };

});