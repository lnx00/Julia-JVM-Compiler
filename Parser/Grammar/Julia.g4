grammar Julia;

/* Parser */

/* Lexer */

fragment Letter: [a-zA-Z];
fragment LetterOrDigit: [a-zA-Z0-9];

// Tokens
FUNCTION_T: 'function';
RETURN_T: 'return';
BEGIN_T: 'begin';
END_T: 'end';
IF_T: 'if';
ELSE_T: 'else';
WHILE_T: 'while';
TRUE_T: 'true';
FALSE_T: 'false';

// Brackets and punctuation
LBRACE: '{';
RBRACE: '}';
LPAREN: '(';
RPAREN: ')';
LBRACK: '[';
RBRACK: ']';
DCOLON: '::';
COMMA: ',';
SEMICOLON: ';';
COLON: ':';
DOT: '.';

// Arithmetic operators
PLUS: '+';
MINUS: '-';
STAR: '*';
SLASH: '/';
PERCENT: '%';

// Comparison operators
EQEQ: '==';
LT: '<';
GT: '>';
LTE: '<=';
GTE: '>=';
NEQ: '!=';

// Boolean operators
AND: '&&';
OR: '||';
NOT: '!';

// Assignment operators
EQ: '=';

IDENTIFIER: Letter LetterOrDigit*;

// Literals
INT_L: MINUS? [0-9]+;
FLOAT_L: MINUS? [0-9]+ DOT [0-9]+;
STRING_L: '"' .*? '"';
BOOL_L: TRUE_T | FALSE_T;

// Comments and whitespace
COMMENT: '#' ~[\r\n]* -> skip;
COMMENT_BLOCK: '#=' .*? '=#' -> skip;
WS: [ \t\r\n]+ -> skip;
