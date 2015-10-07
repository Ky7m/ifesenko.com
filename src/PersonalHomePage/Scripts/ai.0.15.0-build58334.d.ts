declare module Microsoft.ApplicationInsights {
    enum LoggingSeverity {
        CRITICAL = 0,
        WARNING = 1,
    }
    class _InternalLogging {
        private static AiUserActionablePrefix;
        private static AiNonUserActionablePrefix;
        static enableDebugExceptions: () => boolean;
        static verboseLogging: () => boolean;
        static queue: any[];
        private static MAX_INTERNAL_MESSAGE_LIMIT;
        private static _messageCount;
        static throwInternalNonUserActionable(severity: LoggingSeverity, message: string): void;
        static throwInternalUserActionable(severity: LoggingSeverity, message: string): void;
        static warnToConsole(message: string): void;
        static resetInternalMessageCount(): void;
        static setMaxInternalMessageLimit(limit: number): void;
        static logInternalMessage(severity: LoggingSeverity, message: string): void;
        private static _areInternalMessagesThrottled();
    }
}
declare module Microsoft.ApplicationInsights {
    class Util {
        private static document;
        static NotSpecified: string;
        private static _getStorageObject();
        static canUseLocalStorage(): boolean;
        static getStorage(name: string): string;
        static setStorage(name: string, data: string): boolean;
        static removeStorage(name: string): boolean;
        private static _getSessionStorageObject();
        static canUseSessionStorage(): boolean;
        static getSessionStorage(name: string): string;
        static setSessionStorage(name: string, data: string): boolean;
        static removeSessionStorage(name: string): boolean;
        static setCookie(name: any, value: any): void;
        static stringToBoolOrDefault(str: any): boolean;
        static getCookie(name: any): string;
        static deleteCookie(name: string): void;
        static trim(str: any): string;
        static newGuid(): string;
        static isArray(obj: any): boolean;
        static isError(obj: any): boolean;
        static isDate(obj: any): boolean;
        static toISOStringForIE8(date: Date): string;
        static msToTimeSpan(totalms: number): string;
        static isCrossOriginError(message: string, url: string, lineNumber: number, columnNumber: number, error: Error): boolean;
        static dump(object: any): string;
        static addEventHandler(eventName: string, callback: any): boolean;
    }
}
declare module Microsoft.ApplicationInsights {
    interface ISerializable {
        aiDataContract: any;
    }
    class Serializer {
        static serialize(input: ISerializable): string;
        private static _serializeObject(source, name);
        private static _serializeArray(sources, name);
        private static _serializeStringMap(map, expectedType, name);
    }
}
declare module Microsoft.Telemetry {
    class Base {
        baseType: string;
        constructor();
    }
}
declare module Microsoft.Telemetry {
    class Envelope {
        ver: number;
        name: string;
        time: string;
        sampleRate: number;
        seq: string;
        iKey: string;
        flags: number;
        deviceId: string;
        os: string;
        osVer: string;
        appId: string;
        appVer: string;
        userId: string;
        tags: any;
        data: Base;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry.Common {
    class Envelope extends Microsoft.Telemetry.Envelope implements ISerializable {
        aiDataContract: any;
        constructor(data: Microsoft.Telemetry.Base, name: string);
    }
}
declare module Microsoft.ApplicationInsights.Telemetry.Common {
    class Base extends Microsoft.Telemetry.Base implements ISerializable {
        aiDataContract: {};
    }
}
declare module AI {
    class ContextTagKeys {
        applicationVersion: string;
        applicationBuild: string;
        applicationTypeId: string;
        deviceId: string;
        deviceIp: string;
        deviceLanguage: string;
        deviceLocale: string;
        deviceModel: string;
        deviceNetwork: string;
        deviceNetworkName: string;
        deviceOEMName: string;
        deviceOS: string;
        deviceOSVersion: string;
        deviceRoleInstance: string;
        deviceRoleName: string;
        deviceScreenResolution: string;
        deviceType: string;
        deviceMachineName: string;
        deviceVMName: string;
        locationIp: string;
        operationId: string;
        operationName: string;
        operationParentId: string;
        operationRootId: string;
        operationSyntheticSource: string;
        operationIsSynthetic: string;
        sessionId: string;
        sessionIsFirst: string;
        sessionIsNew: string;
        userAccountAcquisitionDate: string;
        userAccountId: string;
        userAgent: string;
        userId: string;
        userStoreRegion: string;
        userAuthUserId: string;
        userAnonymousUserAcquisitionDate: string;
        userAuthenticatedUserAcquisitionDate: string;
        sampleRate: string;
        internalSdkVersion: string;
        internalAgentVersion: string;
        internalDataCollectorReceivedTime: string;
        internalProfileId: string;
        internalProfileClassId: string;
        internalAccountId: string;
        internalApplicationName: string;
        internalInstrumentationKey: string;
        internalTelemetryItemId: string;
        internalApplicationType: string;
        internalRequestSource: string;
        internalFlowType: string;
        internalIsAudit: string;
        internalTrackingSourceId: string;
        internalTrackingType: string;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Context {
    class Application {
        ver: string;
        build: string;
    }
}
declare module Microsoft.ApplicationInsights.Context {
    class Device {
        type: string;
        id: string;
        oemName: string;
        model: string;
        network: number;
        resolution: string;
        locale: string;
        ip: string;
        language: string;
        os: string;
        osversion: string;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Context {
    class Internal {
        sdkVersion: string;
        agentVersion: string;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Context {
    class Location {
        ip: string;
    }
}
declare module Microsoft.ApplicationInsights.Context {
    class Operation {
        id: string;
        name: string;
        parentId: string;
        rootId: string;
        syntheticSource: string;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights {
    class SamplingScoreGenerator {
        static INT_MAX_VALUE: number;
        static getScore(envelope: Telemetry.Common.Envelope): number;
        static getSamplingHashCode(input: string): number;
    }
}
declare module Microsoft.ApplicationInsights.Context {
    class Sample {
        sampleRate: number;
        INT_MAX_VALUE: number;
        constructor(sampleRate: number);
        isSampledIn(envelope: Telemetry.Common.Envelope): boolean;
    }
}
declare module AI {
    enum SessionState {
        Start = 0,
        End = 1,
    }
}
declare module Microsoft.ApplicationInsights.Context {
    interface ISessionConfig {
        sessionRenewalMs: () => number;
        sessionExpirationMs: () => number;
    }
    class Session {
        id: string;
        isFirst: boolean;
        acquisitionDate: number;
        renewalDate: number;
    }
    class _SessionManager {
        static acquisitionSpan: number;
        static renewalSpan: number;
        automaticSession: Session;
        config: ISessionConfig;
        _sessionHandler: (sessionState: AI.SessionState, timestamp: number) => void;
        constructor(config: ISessionConfig, sessionHandler: (sessionState: AI.SessionState, timestamp: number) => void);
        update(): void;
        backup(): void;
        private initializeAutomaticSession();
        private initializeAutomaticSessionWithData(sessionData);
        private renew();
        private setCookie(guid, acq, renewal);
        private setStorage(guid, acq, renewal);
    }
}
declare module Microsoft.ApplicationInsights.Context {
    class User {
        static cookieSeparator: string;
        static userCookieName: string;
        static authUserCookieName: string;
        id: string;
        authenticatedId: string;
        accountId: string;
        accountAcquisitionDate: string;
        agent: string;
        storeRegion: string;
        setAuthenticatedUserContext(authenticatedUserId: string, accountId?: string): void;
        clearAuthenticatedUserContext(): void;
        constructor(accountId: string);
        private validateUserInput(id);
    }
}
declare module Microsoft.ApplicationInsights {
    interface ISenderConfig {
        endpointUrl: () => string;
        emitLineDelimitedJson: () => boolean;
        maxBatchSizeInBytes: () => number;
        maxBatchInterval: () => number;
        disableTelemetry: () => boolean;
    }
    class Sender {
        private _buffer;
        private _lastSend;
        private _timeoutHandle;
        _config: ISenderConfig;
        _sender: (payload: string, isAsync: boolean) => void;
        constructor(config: ISenderConfig);
        send(envelope: Telemetry.Common.Envelope): void;
        private _getSizeInBytes(list);
        triggerSend(async?: boolean): void;
        private _xhrSender(payload, isAsync);
        private _xdrSender(payload, isAsync);
        static _xhrReadyStateChange(xhr: XMLHttpRequest, payload: string): void;
        static _xdrOnLoad(xdr: any, payload: string): void;
        static _onError(payload: string, message: string, event?: ErrorEvent): void;
        static _onSuccess(payload: string): void;
    }
}
declare module Microsoft.Telemetry {
    class Domain {
        constructor();
    }
}
declare module AI {
    enum SeverityLevel {
        Verbose = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
        Critical = 4,
    }
}
declare module AI {
    class MessageData extends Microsoft.Telemetry.Domain {
        ver: number;
        message: string;
        severityLevel: AI.SeverityLevel;
        properties: any;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry.Common {
    class DataSanitizer {
        private static MAX_NAME_LENGTH;
        private static MAX_STRING_LENGTH;
        private static MAX_URL_LENGTH;
        private static MAX_MESSAGE_LENGTH;
        private static MAX_EXCEPTION_LENGTH;
        static sanitizeKeyAndAddUniqueness(key: any, map: any): any;
        static sanitizeKey(name: any): any;
        static sanitizeString(value: any): any;
        static sanitizeUrl(url: any): any;
        static sanitizeMessage(message: any): any;
        static sanitizeException(exception: any): any;
        static sanitizeProperties(properties: any): any;
        static sanitizeMeasurements(measurements: any): any;
        static padNumber(num: any): string;
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class Trace extends AI.MessageData implements ISerializable {
        static envelopeType: string;
        static dataType: string;
        aiDataContract: {
            ver: boolean;
            message: boolean;
            severityLevel: boolean;
            measurements: boolean;
            properties: boolean;
        };
        constructor(message: string, properties?: Object);
    }
}
declare module AI {
    class EventData extends Microsoft.Telemetry.Domain {
        ver: number;
        name: string;
        properties: any;
        measurements: any;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class Event extends AI.EventData implements ISerializable {
        static envelopeType: string;
        static dataType: string;
        aiDataContract: {
            ver: boolean;
            name: boolean;
            properties: boolean;
            measurements: boolean;
        };
        constructor(name: string, properties?: Object, measurements?: Object);
    }
}
declare module AI {
    class ExceptionDetails {
        id: number;
        outerId: number;
        typeName: string;
        message: string;
        hasFullStack: boolean;
        stack: string;
        parsedStack: StackFrame[];
        constructor();
    }
}
declare module AI {
    class ExceptionData extends Microsoft.Telemetry.Domain {
        ver: number;
        handledAt: string;
        exceptions: ExceptionDetails[];
        severityLevel: AI.SeverityLevel;
        problemId: string;
        crashThreadId: number;
        properties: any;
        measurements: any;
        constructor();
    }
}
declare module AI {
    class StackFrame {
        level: number;
        method: string;
        assembly: string;
        fileName: string;
        line: number;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class Exception extends AI.ExceptionData implements ISerializable {
        static envelopeType: string;
        static dataType: string;
        aiDataContract: {
            ver: boolean;
            handledAt: boolean;
            exceptions: boolean;
            severityLevel: boolean;
            properties: boolean;
            measurements: boolean;
        };
        constructor(exception: Error, handledAt?: string, properties?: Object, measurements?: Object);
        static CreateSimpleException(message: string, typeName: string, assembly: string, fileName: string, details: string, line: number, handledAt?: string): Telemetry.Exception;
    }
}
declare module AI {
    class MetricData extends Microsoft.Telemetry.Domain {
        ver: number;
        metrics: DataPoint[];
        properties: any;
        constructor();
    }
}
declare module AI {
    enum DataPointType {
        Measurement = 0,
        Aggregation = 1,
    }
}
declare module AI {
    class DataPoint {
        name: string;
        kind: AI.DataPointType;
        value: number;
        count: number;
        min: number;
        max: number;
        stdDev: number;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry.Common {
    class DataPoint extends AI.DataPoint implements ISerializable {
        aiDataContract: {
            name: boolean;
            kind: boolean;
            value: boolean;
            count: boolean;
            min: boolean;
            max: boolean;
            stdDev: boolean;
        };
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class Metric extends AI.MetricData implements ISerializable {
        static envelopeType: string;
        static dataType: string;
        aiDataContract: {
            ver: boolean;
            metrics: boolean;
            properties: boolean;
        };
        constructor(name: string, value: number, count?: number, min?: number, max?: number, properties?: Object);
    }
}
declare module AI {
    class PageViewData extends AI.EventData {
        ver: number;
        url: string;
        name: string;
        duration: string;
        referrer: string;
        referrerData: string;
        properties: any;
        measurements: any;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class PageView extends AI.PageViewData implements ISerializable {
        static envelopeType: string;
        static dataType: string;
        aiDataContract: {
            ver: boolean;
            name: boolean;
            url: boolean;
            duration: boolean;
            properties: boolean;
            measurements: boolean;
        };
        constructor(name?: string, url?: string, durationMs?: number, properties?: any, measurements?: any);
    }
}
declare module AI {
    class PageViewPerfData extends AI.PageViewData {
        ver: number;
        url: string;
        perfTotal: string;
        name: string;
        duration: string;
        networkConnect: string;
        referrer: string;
        sentRequest: string;
        referrerData: string;
        receivedResponse: string;
        domProcessing: string;
        properties: any;
        measurements: any;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class PageViewPerformance extends AI.PageViewPerfData implements ISerializable {
        static envelopeType: string;
        static dataType: string;
        aiDataContract: {
            ver: boolean;
            name: boolean;
            url: boolean;
            duration: boolean;
            perfTotal: boolean;
            networkConnect: boolean;
            sentRequest: boolean;
            receivedResponse: boolean;
            domProcessing: boolean;
            properties: boolean;
            measurements: boolean;
        };
        isValid: boolean;
        constructor(name: string, url: string, durationMs: number, properties?: any, measurements?: any);
        static getPerformanceTiming(): PerformanceTiming;
        static isPerformanceTimingSupported(): PerformanceTiming;
        static isPerformanceTimingDataReady(): boolean;
        static getDuration(start: any, end: any): number;
    }
}
declare module AI {
    class SessionStateData extends Microsoft.Telemetry.Domain {
        ver: number;
        state: AI.SessionState;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class SessionTelemetry extends AI.SessionStateData implements ISerializable {
        static envelopeType: string;
        static dataType: string;
        aiDataContract: {
            ver: boolean;
            state: boolean;
        };
        constructor(state: AI.SessionState);
    }
}
declare module Microsoft.ApplicationInsights {
    interface ITelemetryConfig extends ISenderConfig {
        instrumentationKey: () => string;
        accountId: () => string;
        sessionRenewalMs: () => number;
        sessionExpirationMs: () => number;
        sampleRate: () => number;
    }
    class TelemetryContext {
        _config: ITelemetryConfig;
        _sender: Sender;
        application: Context.Application;
        device: Context.Device;
        internal: Context.Internal;
        location: Context.Location;
        operation: Context.Operation;
        sample: Context.Sample;
        user: Context.User;
        session: Context.Session;
        private telemetryInitializers;
        _sessionManager: Microsoft.ApplicationInsights.Context._SessionManager;
        constructor(config: ITelemetryConfig);
        addTelemetryInitializer(telemetryInitializer: (envelope: Telemetry.Common.Envelope) => void): void;
        track(envelope: Telemetry.Common.Envelope): Telemetry.Common.Envelope;
        private _track(envelope);
        private static _sessionHandler(tc, sessionState, timestamp);
        private _applyApplicationContext(envelope, appContext);
        private _applyDeviceContext(envelope, deviceContext);
        private _applyInternalContext(envelope, internalContext);
        private _applyLocationContext(envelope, locationContext);
        private _applyOperationContext(envelope, operationContext);
        private _applySampleContext(envelope, sampleContext);
        private _applySessionContext(envelope, sessionContext);
        private _applyUserContext(envelope, userContext);
    }
}
declare module Microsoft.Telemetry {
    class Data<TDomain> extends Microsoft.Telemetry.Base {
        baseType: string;
        baseData: TDomain;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights.Telemetry.Common {
    class Data<TDomain> extends Microsoft.Telemetry.Data<TDomain> implements ISerializable {
        aiDataContract: {
            baseType: boolean;
            baseData: boolean;
        };
        constructor(type: string, data: TDomain);
    }
}
declare module Microsoft.ApplicationInsights.Telemetry {
    class PageVisitTimeManager {
        private prevPageVisitDataKeyName;
        private pageVisitTimeTrackingHandler;
        constructor(pageVisitTimeTrackingHandler: (pageName: string, pageUrl: string, pageVisitTime: number) => void);
        trackPreviousPageVisit(currentPageName: string, currentPageUrl: string): void;
        restartPageVisitTimer(pageName: string, pageUrl: string): PageVisitData;
        startPageVisitTimer(pageName: string, pageUrl: string): void;
        stopPageVisitTimer(): PageVisitData;
    }
    class PageVisitData {
        pageName: string;
        pageUrl: string;
        pageVisitStartTime: number;
        pageVisitTime: number;
        constructor(pageName: any, pageUrl: any);
    }
}
declare module Microsoft.ApplicationInsights {
    var Version: string;
    interface IConfig {
        instrumentationKey: string;
        endpointUrl: string;
        emitLineDelimitedJson: boolean;
        accountId: string;
        appUserId: string;
        sessionRenewalMs: number;
        sessionExpirationMs: number;
        maxBatchSizeInBytes: number;
        maxBatchInterval: number;
        enableDebug: boolean;
        autoCollectErrors: boolean;
        disableTelemetry: boolean;
        verboseLogging: boolean;
        diagnosticLogInterval: number;
        samplingPercentage: number;
        autoTrackPageVisitTime: boolean;
    }
    class AppInsights {
        private _eventTracking;
        private _pageTracking;
        private _pageVisitTimeManager;
        config: IConfig;
        context: TelemetryContext;
        static defaultConfig: IConfig;
        constructor(config: IConfig);
        private sendPageViewInternal(name?, url?, duration?, properties?, measurements?);
        startTrackPage(name?: string): void;
        stopTrackPage(name?: string, url?: string, properties?: Object, measurements?: Object): void;
        trackPageView(name?: string, url?: string, properties?: Object, measurements?: Object): void;
        private trackPageViewInternal(name?, url?, properties?, measurements?);
        startTrackEvent(name: string): void;
        stopTrackEvent(name: string, properties?: Object, measurements?: Object): void;
        trackEvent(name: string, properties?: Object, measurements?: Object): void;
        trackException(exception: Error, handledAt?: string, properties?: Object, measurements?: Object): void;
        trackMetric(name: string, average: number, sampleCount?: number, min?: number, max?: number, properties?: Object): void;
        trackTrace(message: string, properties?: Object): void;
        private trackPageVisitTime(pageName, pageUrl, pageVisitTime);
        flush(): void;
        setAuthenticatedUserContext(authenticatedUserId: string, accountId?: string): void;
        clearAuthenticatedUserContext(): void;
        private SendCORSException(properties);
        _onerror(message: string, url: string, lineNumber: number, columnNumber: number, error: Error): void;
    }
}
declare module AI {
    class AjaxCallData extends AI.PageViewData {
        ver: number;
        url: string;
        ajaxUrl: string;
        name: string;
        duration: string;
        requestSize: number;
        referrer: string;
        responseSize: number;
        referrerData: string;
        timeToFirstByte: string;
        timeToLastByte: string;
        callbackDuration: string;
        responseCode: string;
        success: boolean;
        properties: any;
        measurements: any;
        constructor();
    }
}
declare module AI {
    enum DependencyKind {
        SQL = 0,
        Http = 1,
        Other = 2,
    }
}
declare module AI {
    enum DependencySourceType {
        Undefined = 0,
        Aic = 1,
        Apmc = 2,
    }
}
declare module AI {
    class RemoteDependencyData extends Microsoft.Telemetry.Domain {
        ver: number;
        name: string;
        kind: AI.DataPointType;
        value: number;
        count: number;
        min: number;
        max: number;
        stdDev: number;
        dependencyKind: AI.DependencyKind;
        success: boolean;
        async: boolean;
        dependencySource: AI.DependencySourceType;
        commandName: string;
        dependencyTypeName: string;
        properties: any;
        constructor();
    }
}
declare module AI {
    class RequestData extends Microsoft.Telemetry.Domain {
        ver: number;
        id: string;
        name: string;
        startTime: string;
        duration: string;
        responseCode: string;
        success: boolean;
        httpMethod: string;
        url: string;
        properties: any;
        measurements: any;
        constructor();
    }
}
declare module Microsoft.ApplicationInsights {
    interface Snippet {
        queue: Array<() => void>;
        config: IConfig;
    }
    class Initialization {
        snippet: Snippet;
        config: IConfig;
        constructor(snippet: Snippet);
        loadAppInsights(): AppInsights;
        emptyQueue(): void;
        pollInteralLogs(appInsightsInstance: AppInsights): number;
        addHousekeepingBeforeUnload(appInsightsInstance: AppInsights): void;
        static getDefaultConfig(config?: IConfig): IConfig;
    }
}
declare function initializeAppInsights(): void;
